using System;
using System.Collections.Generic;
using Fohjin.DDD.EventStore.Storage.Memento;

namespace Fohjin.DDD.EventStore.Storage
{
    public class EventStoreIdentityMap<TDomainEvent> : IIdentityMap<TDomainEvent> where TDomainEvent : IDomainEvent
    {
        private readonly Dictionary<Type, Dictionary<Guid, object>> _identityMap;

        public EventStoreIdentityMap()
        {
            _identityMap = new Dictionary<Type, Dictionary<Guid, object>>();
        }

        public TAggregate GetById<TAggregate>(Guid id) where TAggregate : class, IOrginator, IEventProvider<TDomainEvent>, new()
        {
            Dictionary<Guid, object> aggregates;
            if (!_identityMap.TryGetValue(typeof(TAggregate), out aggregates))
                return null;

            object aggregate;
            if (!aggregates.TryGetValue(id, out aggregate))
                return null;

            return (TAggregate) aggregate;
        }

        public void Add<TAggregate>(TAggregate aggregateRoot) where TAggregate : class, IOrginator, IEventProvider<TDomainEvent>, new()
        {
            Dictionary<Guid, object> aggregates;
            if (!_identityMap.TryGetValue(typeof(TAggregate), out aggregates))
            {
                aggregates = new Dictionary<Guid, object>();
                _identityMap.Add(typeof(TAggregate), aggregates);
            }

            if (aggregates.ContainsKey(aggregateRoot.Id))
                return;

            aggregates.Add(aggregateRoot.Id, aggregateRoot);
        }

        public void Remove(Type aggregateRootType, Guid aggregateRootId)
        {
            Dictionary<Guid, object> aggregates;
            if (!_identityMap.TryGetValue(aggregateRootType, out aggregates))
                return;

            aggregates.Remove(aggregateRootId);
        }
    }
}