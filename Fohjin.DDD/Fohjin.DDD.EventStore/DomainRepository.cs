using System;
using Fohjin.DDD.EventStore.Storage;
using Fohjin.DDD.EventStore.Storage.Memento;

namespace Fohjin.DDD.EventStore
{
    public class DomainRepository : IDomainRepository
    {
        private readonly IEventStoreUnitOfWork _eventStoreUnitOfWork;
        private readonly IIdentityMap _identityMap;

        public DomainRepository(IEventStoreUnitOfWork eventStoreUnitOfWork, IIdentityMap identityMap)
        {
            _eventStoreUnitOfWork = eventStoreUnitOfWork;
            _identityMap = identityMap;
        }

        public TAggregate GetById<TAggregate>(Guid id) where TAggregate : class, IOrginator, IEventProvider, new()
        {
            return RegisterForTracking(_identityMap.GetById<TAggregate>(id)) ?? _eventStoreUnitOfWork.GetById<TAggregate>(id);
        }

        public void Add<TAggregate>(TAggregate aggregateRoot) where TAggregate : class, IOrginator, IEventProvider, new()
        {
            _eventStoreUnitOfWork.Add(aggregateRoot);
        }

        private TAggregate RegisterForTracking<TAggregate>(TAggregate aggregateRoot) where TAggregate : class, IOrginator, IEventProvider, new()
        {
            if (aggregateRoot == null)
                return aggregateRoot;

            _eventStoreUnitOfWork.RegisterForTracking(aggregateRoot);
            return aggregateRoot;
        }
    }
}