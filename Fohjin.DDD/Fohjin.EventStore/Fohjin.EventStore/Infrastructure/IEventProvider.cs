using System;
using System.Linq;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Reflection;
using Castle.Core.Interceptor;

namespace Fohjin.EventStore.Infrastructure
{
    public interface IEventProvider
    {
        Guid Id { get; }
        int Version { get; }
        IEnumerable<IDomainEvent> GetChanges();

        void Clear();
        void LoadFromHistory(IEnumerable<IDomainEvent> domainEvents);
        void UpdateVersion(int version);
    }

    public class EventProvider : IEventProvider, IInterceptor
    {
        private readonly Type _hostType;
        private readonly List<IDomainEvent> _appliedEvents;
        private Dictionary<Type, List<Action<object>>> _registeredEventHandlers;
        private readonly Dictionary<string, object> _internalState;
        private static readonly MethodInfo _buildEventPropertyAccessor;
        private static readonly MethodInfo _buildLambda;

        public Guid Id { get; protected set; }
        public int Version { get; protected set; }
        public int EventVersion { get; protected set; }

        static EventProvider()
        {
            _buildEventPropertyAccessor = typeof(EventProvider).GetMethod("CreateToInternalStateCopyLambda", BindingFlags.Instance | BindingFlags.NonPublic);
            _buildLambda = typeof(EventProvider).GetMethod("CreateEventPropertyAccessorLabmda", BindingFlags.Instance | BindingFlags.NonPublic);
        }

        public EventProvider(Type hostType)
        {
            _hostType = hostType;
            EventVersion = 0;
            _appliedEvents = new List<IDomainEvent>();
            _registeredEventHandlers = new Dictionary<Type, List<Action<object>>>();
            _internalState = new Dictionary<string, object>();
        }

        IEnumerable<IDomainEvent> IEventProvider.GetChanges()
        {
            return _appliedEvents;
        }

        void IEventProvider.Clear()
        {
            _appliedEvents.Clear();
        }

        void IEventProvider.LoadFromHistory(IEnumerable<IDomainEvent> domainEvents)
        {
            if (domainEvents.Count() == 0)
                return;

            foreach (var domainEvent in domainEvents)
            {
                apply(domainEvent.GetType(), domainEvent);
            }

            Version = domainEvents.Last().EventVersion;
            EventVersion = Version;
        }

        void IEventProvider.UpdateVersion(int version)
        {
            Version = version;
        }

        void IInterceptor.Intercept(IInvocation invocation)
        {
            if (invocation.Method.Name == "Apply")
            {
                var domainEvent = (IDomainEvent)invocation.Arguments.First();

                domainEvent.AggregateId = Id;
                domainEvent.EventVersion = ++EventVersion;
                apply(domainEvent.GetType(), domainEvent);
                _appliedEvents.Add(domainEvent);
                return;
            }
            if (invocation.Method.DeclaringType == _hostType && invocation.Method.Name.StartsWith("get_") && _internalState.ContainsKey(invocation.Method.Name.Substring(4)))
            {
                invocation.ReturnValue = _internalState[invocation.Method.Name.Substring(4)];
                return;
            }
            invocation.Proceed();
        }

        private void apply(Type eventType, IDomainEvent domainEvent)
        {
            List<Action<object>> handlers;

            if (!_registeredEventHandlers.TryGetValue(domainEvent.GetType(), out handlers))
                throw new UnregisteredDomainEventException(string.Format("The requested domain event '{0}' is not registered in '{1}'", eventType.FullName, GetType().FullName));

            foreach (var handler in handlers)
            {
                handler(domainEvent);
            }
        }

        public void RegisterEventHandlers(object proxy, Type hostType)
        {
            var registeredEvents = GetRegisteredEvents(hostType, proxy);
            ProcessRegisteredEvents(registeredEvents);
        }

        private static IEnumerable<Type> GetRegisteredEvents(IReflect proxyType, object proxy)
        {
            return (IEnumerable<Type>) proxyType
                .GetMethod("RegisteredEvents", BindingFlags.Instance | BindingFlags.NonPublic)
                .Invoke(proxy, new object[] { });
        }

        private void ProcessRegisteredEvents(IEnumerable<Type> registeredEvents)
        {
            foreach (var registeredEvent in registeredEvents)
            {
                var interfaceProperties = typeof (IDomainEvent).GetProperties().Select(x => x.Name);
                var eventProperties = registeredEvent.GetProperties(BindingFlags.Instance | BindingFlags.Public).Where(x => !interfaceProperties.Contains(x.Name)).ToList();
                if (eventProperties.Count() == 0)
                    continue;

                _registeredEventHandlers.Add(registeredEvent, new List<Action<object>>());
                ProcessEventProperties(registeredEvent, eventProperties);
            }
        }

        private void ProcessEventProperties(Type registeredEvent, IEnumerable<PropertyInfo> eventProperties)
        {
            foreach (var eventProperty in eventProperties)
            {
                var invoke = _buildLambda.MakeGenericMethod(registeredEvent).Invoke(this, new object[] { eventProperty });

                var makeGenericMethod = _buildEventPropertyAccessor.MakeGenericMethod(registeredEvent, eventProperty.PropertyType);
                var func = makeGenericMethod.Invoke(this, new [] { eventProperty, invoke }) as Action<object>;

                _registeredEventHandlers[registeredEvent].Add(func);
            }
        }

        private Action<object> CreateToInternalStateCopyLambda<TEventType, TPropertyType>(MemberInfo property, object func) where TEventType : class, IDomainEvent
        {
            return eventType =>
            {
                if (!_internalState.ContainsKey(property.Name))
                    _internalState.Add(property.Name, new object());

                _internalState[property.Name] = (TPropertyType) ((Expression<Func<TEventType, TPropertyType>>) func).Compile().Invoke(eventType as TEventType);
            };
        }

        private object CreateEventPropertyAccessorLabmda<TEventType>(MemberInfo property)
        {
            var expression = Expression.Parameter(typeof(TEventType), "x");
            return Expression.Lambda(Expression.MakeMemberAccess(expression, property), new[] { expression });
        }

        public void SetRegisteredEventHandlers(Dictionary<Type, List<Action<object>>> cache)
        {
            _registeredEventHandlers = cache;
        }

        public Dictionary<Type, List<Action<object>>> GetRegisteredEventHandlers()
        {
            return _registeredEventHandlers;
        }
    }
}