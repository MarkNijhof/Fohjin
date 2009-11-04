using System;
using System.Linq;

namespace Fohjin.DDD.EventStore
{
    public class DomainRepository : IDomainRepository
    {
        private readonly IDomainEventStorage _domainEventStorage;

        public DomainRepository(IDomainEventStorage domainEventStorage)
        {
            _domainEventStorage = domainEventStorage;
        }

        public TAggregate GetById<TAggregate>(Guid id) where TAggregate : IOrginator, IEventProvider, new()
        {
            var aggregateRoot = new TAggregate();

            LoadSnapShotIfExists(id, aggregateRoot);

            loadRemainingHistoryEvents(id, aggregateRoot);

            return aggregateRoot;
        }

        public void Save<TAggregate>(TAggregate aggregateRoot) where TAggregate : IOrginator, IEventProvider, new()
        {
            var entity = (IEventProvider) aggregateRoot;

            _domainEventStorage.Save(entity);

            entity.Clear();
        }

        private void LoadSnapShotIfExists(Guid id, IOrginator aggregateRoot)
        {
            var snapShot = _domainEventStorage.GetSnapShot(id);
            if (snapShot == null)
                return;

            aggregateRoot.SetMemento(snapShot.Memento);
        }

        private void loadRemainingHistoryEvents(Guid id, IEventProvider aggregateRoot)
        {
            var events = _domainEventStorage.GetEventsSinceLastSnapShot(id);
            if (events.Count() > 0)
            {
                aggregateRoot.LoadFromHistory(events);
                return;
            }

            aggregateRoot.LoadFromHistory(_domainEventStorage.GetAllEvents(id));
        }
    }
}