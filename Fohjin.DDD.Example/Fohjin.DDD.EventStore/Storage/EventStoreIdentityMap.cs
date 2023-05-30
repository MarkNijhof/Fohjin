using Fohjin.DDD.EventStore.Storage.Memento;

namespace Fohjin.DDD.EventStore.Storage
{
    public class EventStoreIdentityMap<TDomainEvent> : IIdentityMap<TDomainEvent> where TDomainEvent : IDomainEvent
    {
        private readonly Dictionary<Type, Dictionary<Guid, object>> _identityMap = new ();


        public TAggregate GetById<TAggregate>(Guid id) where TAggregate : class, IOriginator, IEventProvider<TDomainEvent>, new()
        {
            if (!_identityMap.TryGetValue(typeof(TAggregate), out Dictionary<Guid, object> aggregates))
                return null;

            if (!aggregates.TryGetValue(id, out object aggregate))
                return null;

            return (TAggregate)aggregate;
        }

        public void Add<TAggregate>(TAggregate aggregateRoot) where TAggregate : class, IOriginator, IEventProvider<TDomainEvent>, new()
        {
            if (!_identityMap.TryGetValue(typeof(TAggregate), out Dictionary<Guid, object> aggregates))
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
            if (!_identityMap.TryGetValue(aggregateRootType, out Dictionary<Guid, object> aggregates))
                return;

            aggregates.Remove(aggregateRootId);
        }
    }
}