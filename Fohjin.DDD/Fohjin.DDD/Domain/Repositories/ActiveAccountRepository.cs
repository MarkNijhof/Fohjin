using System;
using System.Linq;
using Fohjin.DDD.Domain.Entities;
using Fohjin.DDD.Domain.Memento;

namespace Fohjin.DDD.Domain.Repositories
{
    public class ActiveAccountRepository
    {
        private readonly IDomainEventStorage _domainEventStorage;
        private readonly ISnapShotStorage _snapShotStorage;

        public ActiveAccountRepository(IDomainEventStorage domainEventStorage, ISnapShotStorage snapShotStorage)
        {
            _snapShotStorage = snapShotStorage;
            _domainEventStorage = domainEventStorage;
        }

        public IActiveAccount GetById(Guid id)
        {
            if (_domainEventStorage.GetEvents().Count() == 0)
                throw new Exception(string.Format("ActiveAccount with id {0} was not found", id));

            var activeAccount = new ActiveAccount();

            var startIndex = LoadSnapShotIfExists(activeAccount);

            loadRemainingHistoryEvents(activeAccount, startIndex);

            return activeAccount;
        }

        public void Save(IActiveAccount activeAccount)
        {
            var entity = (IExposeMyInternalChanges) activeAccount;
            foreach (var domainEvent in entity.GetChanges())
            {
                _domainEventStorage.AddEvent(domainEvent);
                makeSnapShot(entity);
            }
            entity.Clear();
        }

        private void makeSnapShot(IExposeMyInternalChanges activeAccount)
        {
            if (_domainEventStorage.GetEvents().Count() % 10 != 0)
                return;

            var orginator = (IOrginator) activeAccount;
            _snapShotStorage.Add(_domainEventStorage.GetEvents().Count(), orginator.CreateMemento());
        }

        private int LoadSnapShotIfExists(IOrginator activeAccount)
        {
            var startIndex = -1;
            if (_snapShotStorage.HasSnapShots())
            {
                var keyValuePair = _snapShotStorage.GetLastSnapShot();
                startIndex = keyValuePair.Key;
                activeAccount.SetMemento(keyValuePair.Value);
            }
            return startIndex;
        }

        private void loadRemainingHistoryEvents(IExposeMyInternalChanges activeAccount, int startIndex)
        {
            if (_domainEventStorage.GetEvents().Count() > startIndex)
            {
                activeAccount.LoadHistory(_domainEventStorage.GetEvents().SkipWhile((o, i) => i <= startIndex));
            }
        }
    }
}