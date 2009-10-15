using System;
using System.Linq;
using Fohjin.DDD.Domain;
using Fohjin.DDD.Domain.Entities.Mementos;

namespace Fohjin.EventStorage
{
    public class Repository : IRepository
    {
        private readonly IDomainEventStorage _domainEventStorage;
        private readonly ISnapShotStorage _snapShotStorage;

        public Repository(IDomainEventStorage domainEventStorage, ISnapShotStorage snapShotStorage)
        {
            _snapShotStorage = snapShotStorage;
            _domainEventStorage = domainEventStorage;
        }

        public TAggregate GetById<TAggregate>(Guid id) where TAggregate : IOrginator, IEventProvider, new()
        {
            var activeAccount = new TAggregate();

            LoadSnapShotIfExists(id, activeAccount);

            loadRemainingHistoryEvents(id, activeAccount);

            return activeAccount;
        }

        public void Save<TAggregate>(TAggregate activeAccount) where TAggregate : IOrginator, IEventProvider, new()
        {
            var entity = (IEventProvider) activeAccount;

            _domainEventStorage.Save(entity);

            _snapShotStorage.SaveShapShot(entity);

            entity.Clear();
        }

        private void LoadSnapShotIfExists(Guid id, IOrginator activeAccount)
        {
            var snapShot = _snapShotStorage.GetSnapShot(id);
            if (snapShot == null)
                return;

            activeAccount.SetMemento(snapShot.Memento);
        }

        private void loadRemainingHistoryEvents(Guid id, IEventProvider activeAccount)
        {
            var events = _domainEventStorage.GetEventsSinceLastSnapShot(id);
            if (events.Count() > 0)
            {
                activeAccount.LoadFromHistory(events);
                return;
            }

            activeAccount.LoadFromHistory(_domainEventStorage.GetAllEvents(id));
        }
    }
}