using System;
using System.Linq;
using Fohjin.DDD.Domain.Contracts;
using Fohjin.DDD.Domain.Entities;
using Fohjin.DDD.Domain.Memento;

namespace Fohjin.DDD.Domain.Repositories
{
    public class ActiveAccountRepository
    {
        private readonly IDomainEventStorage _domainEventStorage;
        private readonly ISnapShotStorage _snapShotStorage;
        private readonly IEventPropagator _eventPropagator;

        public ActiveAccountRepository(IDomainEventStorage domainEventStorage, ISnapShotStorage snapShotStorage, IEventPropagator eventPropagator)
        {
            _snapShotStorage = snapShotStorage;
            _eventPropagator = eventPropagator;
            _domainEventStorage = domainEventStorage;
        }

        public IActiveAccount GetById(Guid id)
        {
            var activeAccount = new ActiveAccount();

            LoadSnapShotIfExists(id, activeAccount);

            loadRemainingHistoryEvents(id, activeAccount);

            return activeAccount;
        }

        public void Save(IActiveAccount activeAccount)
        {
            var entity = (IExposeMyInternalChanges) activeAccount;

            _eventPropagator.ForwardEvents(entity.Id, entity.GetChanges());

            _domainEventStorage.SaveEvents(entity.Id, entity.GetChanges());

            _snapShotStorage.SaveShapShot(entity);

            entity.Clear();
        }

        private void LoadSnapShotIfExists(Guid id, IOrginator activeAccount)
        {
            var snapShot = _snapShotStorage.GetLastSnapShot(id);
            if (snapShot == null)
                return;

            activeAccount.SetMemento(snapShot.Memento);
        }

        private void loadRemainingHistoryEvents(Guid id, IExposeMyInternalChanges activeAccount)
        {
            var events = _domainEventStorage.GetEventsSinceLastSnapShot(id);
            if (events.Count() > 0)
            {
                activeAccount.LoadHistory(events);
                return;
            }

            activeAccount.LoadHistory(_domainEventStorage.GetAllEvents(id));
        }
    }
}