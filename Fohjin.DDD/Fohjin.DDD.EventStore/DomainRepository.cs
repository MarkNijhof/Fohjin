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
            return GetFromIdentityMap<TAggregate>(id) ?? _eventStoreUnitOfWork.GetById<TAggregate>(id);
        }

        private TAggregate GetFromIdentityMap<TAggregate>(Guid id) where TAggregate : class, IOrginator, IEventProvider, new()
        {
            var aggregate = _identityMap.GetById<TAggregate>(id);
            if (aggregate == null)
                return null;

            _eventStoreUnitOfWork.RegisterForTracking(aggregate);
            return aggregate;
        }

        public void Add<TAggregate>(TAggregate aggregateRoot) where TAggregate : class, IOrginator, IEventProvider, new()
        {
            _eventStoreUnitOfWork.Add(aggregateRoot);
        }

        public void Complete()
        {
            try
            {
                _eventStoreUnitOfWork.Complete();
                //_eventHandlerUnitOfWork.Complete();
                //_eventHandlerUnitOfWork.Commit();
                _eventStoreUnitOfWork.Commit();
            }
            catch (Exception)
            {
                //_eventHandlerUnitOfWork.Rollback();
                _eventStoreUnitOfWork.Rollback();
                throw;
            }
        }
    }
}