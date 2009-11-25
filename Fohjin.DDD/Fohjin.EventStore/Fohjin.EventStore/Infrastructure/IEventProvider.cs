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
        private readonly List<IDomainEvent> _appliedEvents;
        private Dictionary<Type, List<Action<object, object>>> _registeredEventHandlers;
        private object _proxy;

        public Guid Id { get; protected set; }
        public int Version { get; protected set; }
        public int EventVersion { get; protected set; }

        public EventProvider()
        {
            _registeredEventHandlers = new Dictionary<Type, List<Action<object, object>>>();
            _appliedEvents = new List<IDomainEvent>();
            EventVersion = 0;
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

        public void SetProxy(object proxy, Dictionary<Type, List<Action<object, object>>> registeredEvents)
        {
            _proxy = proxy;
            _registeredEventHandlers = registeredEvents;
        }

        public Dictionary<Type, List<Action<object, object>>> SetProxy(object proxy, Type hostType)
        {
            _proxy = proxy;
            var proxyType = proxy.GetType();
            var proxyProperties = GetProxyProperties(proxyType, hostType);
            var registeredEvents = GetRegisteredEvents(proxyType, proxy);
            ProcessRegisteredEvents(registeredEvents, proxyProperties);
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

        private static IEnumerable<PropertyInfo> GetProxyProperties(IReflect proxyType, Type hostType)
        {
            return proxyType
                .GetProperties(BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.GetProperty)
                .Where(x => x.DeclaringType == hostType);
        }

        private static Action<object, object> CreateAction(PropertyInfo proxyProperty, PropertyInfo eventProperty)
        {
            return (p, e) => proxyProperty.SetValue(p, eventProperty.GetValue(e, new object[] {}), new object[] { });
        }
    }
}