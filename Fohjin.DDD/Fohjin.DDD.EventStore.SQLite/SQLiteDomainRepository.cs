using System;
using System.Linq;
using Fohjin.DDD.Contracts;

namespace Fohjin.DDD.EventStore.SQLite
{
    public class SQLiteDomainRepository : IDomainRepository
    {
        private readonly IDomainEventStorage _domainEventStorage;
        private readonly ISnapShotStorage _snapShotStorage;

        public SQLiteDomainRepository(IDomainEventStorage domainEventStorage, ISnapShotStorage snapShotStorage)
        {
            _snapShotStorage = snapShotStorage;
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

            _snapShotStorage.SaveShapShot(entity);

            entity.Clear();
        }

        private void LoadSnapShotIfExists(Guid id, IOrginator aggregateRoot)
        {
            var snapShot = _snapShotStorage.GetSnapShot(id);
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