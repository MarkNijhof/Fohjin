using System;
using System.Collections.Generic;
using System.Linq;

namespace Fohjin.DDD.EventStore
{
    public class EventStoreUnitOfWork : IEventStoreUnitOfWork
    {
        private readonly IDomainEventStorage _domainEventStorage;
        private readonly IIdentityMap _identityMap;
        private readonly List<IEventProvider> _eventProviders;

        public EventStoreUnitOfWork(IDomainEventStorage domainEventStorage, IIdentityMap identityMap)
        {
            _domainEventStorage = domainEventStorage;
            _identityMap = identityMap;
            _eventProviders = new List<IEventProvider>();
        }

        public TAggregate GetById<TAggregate>(Guid id) where TAggregate : class, IOrginator, IEventProvider, new()
        {
            var aggregateRoot = new TAggregate();

            LoadSnapShotIfExists(id, aggregateRoot);

            loadRemainingHistoryEvents(id, aggregateRoot);

            RegisterForTracking(aggregateRoot);

            return aggregateRoot;
        }

        public void Add<TAggregate>(TAggregate aggregateRoot) where TAggregate : class, IOrginator, IEventProvider, new()
        {
            RegisterForTracking(aggregateRoot);
        }

        public void RegisterForTracking<TAggregate>(TAggregate aggregateRoot) where TAggregate : class, IOrginator, IEventProvider, new()
        {
            _eventProviders.Add(aggregateRoot);
            _identityMap.Add(aggregateRoot);
        }

        public void Complete()
        {
            _domainEventStorage.BeginTransaction();
            try
            {
                foreach (var eventProvider in _eventProviders)
                {
                    _domainEventStorage.Save(eventProvider);
                    eventProvider.Clear();
                }
                _eventProviders.Clear();
            }
            catch (Exception)
            {
                Rollback();
                throw;
            }
        }

        public void Commit()
        {
            _domainEventStorage.Commit();
        }

        public void Rollback()
        {
            _domainEventStorage.Rollback();
            foreach (var eventProvider in _eventProviders)
            {
                _identityMap.Remove(eventProvider.GetType(), eventProvider.Id);
            }
            _eventProviders.Clear();
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

        public void Dispose()
        {
            throw new NotImplementedException();
        }
    }
}