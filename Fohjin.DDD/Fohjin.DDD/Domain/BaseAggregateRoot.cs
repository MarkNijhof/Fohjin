using System;
using System.Collections.Generic;
using Fohjin.DDD.Domain.Events;

namespace Fohjin.DDD.Domain
{
    public class BaseAggregateRoot : IExposeMyInternalChanges
    {
        private readonly Dictionary<Type, Delegate> _events;
        private readonly List<IDomainEvent> _appliedEvents;

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
            apply(domainEvent.GetType(), domainEvent);
            _appliedEvents.Add(domainEvent);
        }

        void IExposeMyInternalChanges.LoadHistory(IEnumerable<IDomainEvent> domainEvents)
        {
            foreach (var domainEvent in domainEvents)
            {
                apply(domainEvent.GetType(), domainEvent);
            }
        }

        IEnumerable<IDomainEvent> IExposeMyInternalChanges.GetChanges()
        {
            return _appliedEvents.AsReadOnly();
        }

        void IExposeMyInternalChanges.Clear()
        {
            _appliedEvents.Clear();
        }

        private void apply(Type eventType, object domainEvent)
        {
            if (!_events.ContainsKey(eventType))
                throw new Exception(string.Format("The requested event {0} is not registered", eventType));

            _events[eventType].DynamicInvoke(new[] { domainEvent });
        }
    }
}