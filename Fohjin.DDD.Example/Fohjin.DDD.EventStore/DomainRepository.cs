using Fohjin.DDD.EventStore.Storage;
using Fohjin.DDD.EventStore.Storage.Memento;
using Microsoft.Extensions.Logging;

namespace Fohjin.DDD.EventStore
{
    public class DomainRepository<TDomainEvent> : IDomainRepository<TDomainEvent> where TDomainEvent : IDomainEvent
    {
        private readonly IEventStoreUnitOfWork<TDomainEvent> _eventStoreUnitOfWork;
        private readonly IIdentityMap<TDomainEvent> _identityMap;
        private readonly ILogger _log;

        public DomainRepository(
            IEventStoreUnitOfWork<TDomainEvent> eventStoreUnitOfWork,
            IIdentityMap<TDomainEvent> identityMap,
            ILogger<DomainRepository<TDomainEvent>> log
            )
        {
            _eventStoreUnitOfWork = eventStoreUnitOfWork;
            _identityMap = identityMap;
            _log = log;
        }

        public TAggregate? GetById<TAggregate>(Guid id)
            where TAggregate : class, IOriginator, IEventProvider<TDomainEvent>, new()
        {
            _log.LogInformation($"{nameof(GetById)}({{{nameof(id)}}})", id);
            return RegisterForTracking(_identityMap.GetById<TAggregate>(id)) ?? _eventStoreUnitOfWork.GetById<TAggregate>(id);
        }

        public void Add<TAggregate>(TAggregate aggregateRoot) where TAggregate : class, IOriginator, IEventProvider<TDomainEvent>, new()
        {
            _log.LogInformation($"{nameof(Add)}({{{nameof(aggregateRoot)}}})", aggregateRoot);
            _eventStoreUnitOfWork.Add(aggregateRoot);
        }

        private TAggregate? RegisterForTracking<TAggregate>(TAggregate? aggregateRoot)
            where TAggregate : class, IOriginator, IEventProvider<TDomainEvent>, new()
        {
            if (aggregateRoot == null)
                return aggregateRoot;

            _eventStoreUnitOfWork.RegisterForTracking(aggregateRoot);
            return aggregateRoot;
        }
    }
}