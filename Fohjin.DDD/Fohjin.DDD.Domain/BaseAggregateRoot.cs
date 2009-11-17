using System;
using System.Collections.Generic;
using System.Linq;
using Fohjin.DDD.EventStore;

namespace Fohjin.DDD.Domain
{
    public class BaseAggregateRoot : IEventProvider
    {
        private readonly Dictionary<Type, Action<IDomainEvent>> _events;
        private readonly List<IDomainEvent> _appliedEvents;
        private readonly List<IEntityEventProvider> _childEventProviders;

        public Guid Id { get; protected set; }
        public int Version { get; protected set; }
        public int EventVersion { get; protected set; }

        public BaseAggregateRoot()
        {
            _events = new Dictionary<Type, Action<IDomainEvent>>();
            _appliedEvents = new List<IDomainEvent>();
            _childEventProviders = new List<IEntityEventProvider>();
        }

        protected void RegisterEvent<TEvent>(Action<TEvent> eventHandler) where TEvent : class, IDomainEvent
        {
            _events.Add(typeof(TEvent), theEvent => eventHandler(theEvent as TEvent));
        }

        protected void Apply<TEvent>(TEvent domainEvent) where TEvent : class, IDomainEvent
        {
            domainEvent.AggregateId = Id;
            domainEvent.Version = GetNewEventVersion();
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
            return _appliedEvents.Concat(GetChildEventsAndUpdateEventVersion()).OrderBy(x => x.Version).ToList();
        }

        private IEnumerable<IDomainEvent> GetChildEventsAndUpdateEventVersion()
        {
            return _childEventProviders.SelectMany(entity => entity.GetChanges());
        }

        void IEventProvider.Clear()
        {
            _childEventProviders.ForEach(x => x.Clear());
            _appliedEvents.Clear();
        }

        void IEventProvider.UpdateVersion(int version)
        {
            Version = version;
        }

        private void apply(Type eventType, IDomainEvent domainEvent)
        {
            Action<IDomainEvent> handler;

            if (!_events.TryGetValue(eventType, out handler))
                throw new UnregisteredDomainEventException(string.Format("The requested domain event '{0}' is not registered in '{1}'", eventType.FullName, GetType().FullName));

            handler(domainEvent);
        }

        private int GetNewEventVersion()
        {
            return ++EventVersion;
        }

        void IEventProvider.RegisterChildEventProvider(IEntityEventProvider entityEventProvider)
        {
            entityEventProvider.HookUpVersionProvider(GetNewEventVersion);
            _childEventProviders.Add(entityEventProvider);
        }
    }
}