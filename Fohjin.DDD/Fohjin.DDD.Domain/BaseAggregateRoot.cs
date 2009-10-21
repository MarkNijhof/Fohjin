using System;
using System.Collections.Generic;
using System.Linq;
using Fohjin.DDD.Domain.Exceptions;
using Fohjin.DDD.Events;

namespace Fohjin.DDD.Domain
{
    public class BaseAggregateRoot : IEventProvider
    {
        private readonly Dictionary<Type, Delegate> _events;
        private readonly List<IDomainEvent> _appliedEvents;

        public Guid Id { get; protected set; }
        public int Version { get; protected set; }
        public int EventVersion { get; protected set; }

        public BaseAggregateRoot()
        {
            _events = new Dictionary<Type, Delegate>();
            _appliedEvents = new List<IDomainEvent>();
        }

        protected void RegisterEvent<TEvent>(Action<TEvent> eventHandler) where TEvent : class, IDomainEvent
        {
            _events.Add(typeof(TEvent), eventHandler);
        }

        protected void Apply<TEvent>(TEvent domainEvent) where TEvent : class, IDomainEvent
        {
            domainEvent.AggregateId = Id;
            domainEvent.Version = ++EventVersion;
            apply(domainEvent.GetType(), domainEvent);
            _appliedEvents.Add(domainEvent);
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

        IEnumerable<IDomainEvent> IEventProvider.GetChanges()
        {
            return _appliedEvents.AsReadOnly();
        }

        void IEventProvider.Clear()
        {
            _appliedEvents.Clear();
        }

        void IEventProvider.UpdateVersion(int version)
        {
            Version = version;
        }

        private void apply(Type eventType, object domainEvent)
        {
            if (!_events.ContainsKey(eventType))
                throw new DomainEventWasNotRegisteredException(string.Format("The requested event '{0}' is not registered", eventType.FullName));

            _events[eventType].DynamicInvoke(new[] { domainEvent });
        }
    }
}