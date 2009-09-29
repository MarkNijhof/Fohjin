using System;
using System.Collections.Generic;
using System.Linq;
using Fohjin.DDD.Domain.Contracts;
using Fohjin.DDD.Domain.Entities;
using Fohjin.DDD.Domain.Events;
using Fohjin.DDD.Domain.Memento;

namespace Fohjin.DDD.Domain.Repositories
{
    public class ActiveAccountRepository
    {
        private const int snapShotInterval = 10;
        private readonly IDomainEventStorage _domainEventStorage;
        private readonly ISnapShotStorage _snapShotStorage;

        public ActiveAccountRepository(IDomainEventStorage domainEventStorage, ISnapShotStorage snapShotStorage)
        {
            _snapShotStorage = snapShotStorage;
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
            _domainEventStorage.AddEvents(entity.Id, entity.GetChanges());

            makeSnapShot(entity);

            entity.Clear();
        }

        private void makeSnapShot(IExposeMyInternalChanges activeAccount)
        {
            if (_snapShotStorage.GetLastSnapShot(activeAccount.Id) != null)
            {
                var events = _domainEventStorage.GetEventsSinceLastSnapShot(activeAccount.Id);
                if (events.Count() < snapShotInterval)
                    return;
            }
            else
            {
                var events = _domainEventStorage.GetAllEvents(activeAccount.Id);
                if (events.Count() < snapShotInterval)
                    return;
            }

            var orginator = (IOrginator)activeAccount;
            _snapShotStorage.Add(activeAccount.Id, new SnapShot(activeAccount.GetChanges().Last().Id, orginator.CreateMemento()));
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