using System;
using System.Linq;
using System.Collections.Generic;
using System.Reflection;
using Castle.Core.Interceptor;

namespace Fohjin.EventStore
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
        private readonly List<IDomainEvent> _appliedEvents;
        private readonly Dictionary<Type, Action<object, IDomainEvent>> _registeredEvents;

        public Guid Id { get; protected set; }
        public int Version { get; protected set; }
        public int EventVersion { get; protected set; }

        public EventProvider(object host)
        {
            _appliedEvents = new List<IDomainEvent>();
            _registeredEvents = new Dictionary<Type, Action<object, IDomainEvent>>();
            EventVersion = 0;

            registerEventHandlers(host);
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

        public void Intercept(IInvocation invocation)
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
            invocation.Proceed();
        }

        private void apply(Type eventType, IDomainEvent domainEvent)
        {
            Action<object, IDomainEvent> handler;

            if (!_registeredEvents.TryGetValue(typeof(IDomainEvent), out handler))
                throw new UnregisteredDomainEventException(string.Format("The requested domain event '{0}' is not registered in '{1}'", eventType.FullName, GetType().FullName));

            handler(this, domainEvent);
        }

        private void registerEventHandlers(object host)
        {
            var methods = host.GetType().GetMethods(BindingFlags.Instance | BindingFlags.NonPublic);
            methods
                .ToList()
                .Where(SelectPrivateMethods)
                .ToList()
                .ForEach(x => RegisterEvent(x, host));
        }

        private static bool SelectPrivateMethods(MethodInfo x)
        {
            var selectPrivateMethods = x.ReturnType.Name == "Void" &&
                                       x.GetParameters().Count() == 1; //&&
            //x.GetParameters().First().ParameterType.GetInterfaces().Contains(typeof(IDomainEvent));
            return selectPrivateMethods;
        }

        private void RegisterEvent(MethodInfo x, object host)
        {
            _registeredEvents.Add(x.GetParameters().First().ParameterType, (theObject, theEvent) => x.Invoke(host, new[] { theEvent }));
            //GetType()
            //    .GetMethod("RegisterSpecificEvent", BindingFlags.Instance | BindingFlags.NonPublic)
            //    .MakeGenericMethod(x.GetParameters().First().ParameterType)
            //    .Invoke(this, new[] {x});
        }

        private void RegisterSpecificEvent<TEvent>(MethodInfo x) where TEvent : class
        {
        }
    }
}