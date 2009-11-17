using System;
using System.Collections.Generic;
using System.Linq;
using Fohjin.DDD.EventStore;

namespace Fohjin.DDD.Domain
{
    public class BaseEntity : IEntityEventProvider
    {
        public Guid Id { get; protected set; }
        private readonly Dictionary<Type, Action<IDomainEvent>> _events;
        private readonly List<IDomainEvent> _appliedEvents;
        private Func<int> _versionProvider;

        public BaseEntity()
        {
            _events = new Dictionary<Type, Action<IDomainEvent>>();
            _appliedEvents = new List<IDomainEvent>();
        }

        protected void RegisterEvent<TEvent>(Action<TEvent> eventHandler) where TEvent : class, IDomainEvent
        {
            _events.Add(typeof(TEvent), theEvent => eventHandler(theEvent as TEvent));
        }

        protected void Apply<TEvent>(TEvent domainEvent) where TEvent : class, IDomainEvent
        {
            domainEvent.AggregateId = Id;
            domainEvent.Version = _versionProvider();
            apply(domainEvent.GetType(), domainEvent);
            _appliedEvents.Add(domainEvent);
        }

        void IEntityEventProvider.LoadFromHistory(IEnumerable<IDomainEvent> domainEvents)
        {
            if (domainEvents.Count() == 0)
                return;

            foreach (var domainEvent in domainEvents)
            {
                apply(domainEvent.GetType(), domainEvent);
            }
        }

        public void HookUpVersionProvider(Func<int> versionProvider)
        {
            _versionProvider = versionProvider;
        }

        IEnumerable<IDomainEvent> IEntityEventProvider.GetChanges()
        {
            return _appliedEvents;
        }

        void IEntityEventProvider.Clear()
        {
            _appliedEvents.Clear();
        }

        private void apply(Type eventType, IDomainEvent domainEvent)
        {
            Action<IDomainEvent> handler;

            if (!_events.TryGetValue(eventType, out handler))
                throw new UnregisteredDomainEventException(string.Format("The requested domain event '{0}' is not registered in '{1}'", eventType.FullName, GetType().FullName));

            handler(domainEvent);
        }
    }
}