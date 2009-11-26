using System;
using System.Linq;
using System.Collections.Generic;
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
        private object _proxy;
        private readonly List<IDomainEvent> _appliedEvents;
        private Dictionary<Type, List<Action<object, object>>> _registeredEventHandlers;

        public Guid Id { get; protected set; }
        public int Version { get; protected set; }
        public int EventVersion { get; protected set; }

        public EventProvider()
        {
            EventVersion = 0;
            _appliedEvents = new List<IDomainEvent>();
            _registeredEventHandlers = new Dictionary<Type, List<Action<object, object>>>();
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

            Version = domainEvents.Last().Version;
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
                domainEvent.Version = ++EventVersion;
                apply(domainEvent.GetType(), domainEvent);
                _appliedEvents.Add(domainEvent);
                return;
            }
            //if (invocation.Method.Name.StartsWith("get_"))
            //{

            //}
            invocation.Proceed();
        }

        private void apply(Type eventType, IDomainEvent domainEvent)
        {
            List<Action<object, object>> handlers;

            if (!_registeredEventHandlers.TryGetValue(domainEvent.GetType(), out handlers))
                throw new UnregisteredDomainEventException(string.Format("The requested domain event '{0}' is not registered in '{1}'", eventType.FullName, GetType().FullName));

            foreach (var handler in handlers)
            {
                handler(_proxy, domainEvent);
            }
        }

        public void SetProxy(object proxy)
        {
            _proxy = proxy;
        }

        public void SetProxy(object proxy, Type hostType)
        {
            _proxy = proxy;
            var proxyProperties = GetProxyProperties(hostType);
            var registeredEvents = GetRegisteredEvents(hostType, proxy);
            ProcessRegisteredEvents(registeredEvents, proxyProperties);
        }

        public void SetRegisteredEventHandlers(Dictionary<Type, List<Action<object, object>>> cache)
        {
            _registeredEventHandlers = cache;
        }

        public Dictionary<Type, List<Action<object, object>>> GetRegisteredEventHandlers()
        {
            return _registeredEventHandlers;
        }

        private void ProcessRegisteredEvents(IEnumerable<Type> registeredEvents, IEnumerable<PropertyInfo> proxyProperties)
        {
            foreach (var registeredEvent in registeredEvents)
            {
                var eventProperties = registeredEvent.GetProperties(BindingFlags.Instance | BindingFlags.Public);
                if (eventProperties.Count() == 0)
                    continue;

                _registeredEventHandlers.Add(registeredEvent, new List<Action<object, object>>());
                ProcessEventProperties(registeredEvent, eventProperties, proxyProperties);
            }
        }

        private void ProcessEventProperties(Type registeredEvent, IEnumerable<PropertyInfo> eventProperties, IEnumerable<PropertyInfo> proxyProperties)
        {
            foreach (var eventProperty in eventProperties)
            {
                var eventPropertyName = eventProperty.Name;
                var proxyProperty = proxyProperties.Where(x => x.Name == eventPropertyName).FirstOrDefault();
                if (proxyProperty == null)
                    continue;

                _registeredEventHandlers[registeredEvent].Add(CreateAction(proxyProperty, eventProperty));
            }
        }

        private static IEnumerable<Type> GetRegisteredEvents(IReflect proxyType, object proxy)
        {
            return (IEnumerable<Type>)proxyType
                                          .GetMethod("RegisteredEvents", BindingFlags.Instance | BindingFlags.NonPublic)
                                          .Invoke(proxy, new object[] { });
        }

        private static IEnumerable<PropertyInfo> GetProxyProperties(IReflect hostType)
        {
            return hostType
                .GetProperties(BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.GetProperty);
        }

        private static Action<object, object> CreateAction(PropertyInfo proxyProperty, PropertyInfo eventProperty)
        {
            return (p, e) => proxyProperty.SetValue(p, eventProperty.GetValue(e, new object[] {}), new object[] { });
        }
    }
}