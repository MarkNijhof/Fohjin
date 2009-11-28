using System;
using System.Linq;
using System.Collections.Generic;
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
        private readonly IEventRegistrator _eventRegistrator;
        private readonly ICacheRegisteredEvents _cacheRegisteredEvents;
        private readonly List<IDomainEvent> _appliedEvents;
        private readonly Dictionary<string, object> _internalState;
        private Dictionary<Type, List<Action<object, Dictionary<string, object>>>> _registeredEventHandlers;

        public Guid Id { get; protected set; }
        public int Version { get; protected set; }
        public int EventVersion { get; protected set; }

        public EventProvider(Type hostType, IEventRegistrator eventRegistrator, ICacheRegisteredEvents cacheRegisteredEvents)
        {
            _hostType = hostType;
            _eventRegistrator = eventRegistrator;
            _cacheRegisteredEvents = cacheRegisteredEvents;
            EventVersion = 0;
            _appliedEvents = new List<IDomainEvent>();
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

            domainEvents
                .ToList()
                .ForEach(domainEvent => apply(domainEvent.GetType(), domainEvent));

            Version = domainEvents.Last().EventVersion;
            EventVersion = Version;
        }

        void IEventProvider.UpdateVersion(int version)
        {
            Version = version;
        }

        void IInterceptor.Intercept(IInvocation invocation)
        {
            Intercept(invocation);
        }

        private void Intercept(IInvocation invocation)
        {
            if (IsApplyMethod(invocation))
            {
                InterceptApplyMethod(invocation);
                return;
            }
            if (IsInternalStateGetProperty(invocation))
            {
                InterceptInternalStateGetProperty(invocation);
                return;
            }
            if (IsInternalStateSetProperty(invocation))
            {
                InterceptInternalStateSetProperty(invocation);
                return;
            }
            invocation.Proceed();
        }

        public void RegisterEventHandlers(object proxy)
        {
            _registeredEventHandlers = _cacheRegisteredEvents.Get(_hostType);
            if (_registeredEventHandlers != null)
                return;

            _registeredEventHandlers = _eventRegistrator.RegisterEventHandlers(_hostType, proxy);
            _cacheRegisteredEvents.Add(_hostType, _registeredEventHandlers);
        }

        private static bool IsApplyMethod(IInvocation invocation)
        {
            return invocation.Method.Name == "Apply";
        }

        private void InterceptApplyMethod(IInvocation invocation)
        {
            var domainEvent = (IDomainEvent)invocation.Arguments.First();

            domainEvent.AggregateId = Id;
            domainEvent.EventVersion = ++EventVersion;
            apply(domainEvent.GetType(), domainEvent);
            _appliedEvents.Add(domainEvent);
        }

        private bool IsInternalStateGetProperty(IInvocation invocation)
        {
            return
                invocation.Method.DeclaringType == _hostType &&
                invocation.Method.Name.StartsWith("get_") &&
                _internalState.ContainsKey(invocation.Method.Name.Substring(4));
        }

        private void InterceptInternalStateGetProperty(IInvocation invocation)
        {
            invocation.ReturnValue = _internalState[invocation.Method.Name.Substring(4)];
        }

        private bool IsInternalStateSetProperty(IInvocation invocation)
        {
            return
                invocation.Method.DeclaringType == _hostType &&
                invocation.Method.Name.StartsWith("set_");
        }

        private static void InterceptInternalStateSetProperty(IInvocation invocation)
        {
            throw new IlligalStateAssignmentException(string.Format("Internal state is not allowed to be altered directly using property '{0}' and should always be done through the publishing of an event!", invocation.Method.Name.Substring(4)));
        }

        private void apply(Type eventType, IDomainEvent domainEvent)
        {
            List<Action<object, Dictionary<string, object>>> handlers;
            if (!_registeredEventHandlers.TryGetValue(domainEvent.GetType(), out handlers))
                throw new UnregisteredDomainEventException(string.Format("The requested domain event '{0}' is not registered in '{1}'", eventType.FullName, GetType().FullName));

            handlers.ForEach(handler => handler(domainEvent, _internalState));
        }
    }
}