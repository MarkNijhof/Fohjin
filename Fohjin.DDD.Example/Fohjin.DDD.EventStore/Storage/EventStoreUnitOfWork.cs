using Fohjin.DDD.Bus;
using Fohjin.DDD.EventStore.Storage.Memento;
using Microsoft.Extensions.Logging;

namespace Fohjin.DDD.EventStore.Storage
{
    public class EventStoreUnitOfWork<TDomainEvent> : IEventStoreUnitOfWork<TDomainEvent> where TDomainEvent : IDomainEvent
    {
        private static int _seed;
        private readonly int _id = _seed++;

        private readonly IDomainEventStorage<TDomainEvent> _domainEventStorage;
        private readonly IIdentityMap<TDomainEvent> _identityMap;
        private readonly IBus _bus;
        private readonly List<IEventProvider<TDomainEvent>> _eventProviders = new ();
        private readonly ILogger _log;

        public EventStoreUnitOfWork(
            IDomainEventStorage<TDomainEvent> domainEventStorage, 
            IIdentityMap<TDomainEvent> identityMap,
            IBus bus,
            ILogger<EventStoreUnitOfWork<TDomainEvent>> log
            )
        {
            _domainEventStorage = domainEventStorage;
            _identityMap = identityMap;
            _bus = bus;
            _log = log;
        }

        public TAggregate? GetById<TAggregate>(Guid id) where TAggregate : class, IOriginator, IEventProvider<TDomainEvent>, new()
        {
            _log.LogInformation($"{nameof(GetById)}({{{nameof(id)}}})", id);
            var aggregateRoot = new TAggregate();

            LoadSnapShotIfExists(id, aggregateRoot);
            LoadRemainingHistoryEvents(id, aggregateRoot);
            RegisterForTracking(aggregateRoot);

            return aggregateRoot;
        }

        public void Add<TAggregate>(TAggregate aggregateRoot) where TAggregate : class, IOriginator, IEventProvider<TDomainEvent>, new()
        {
            _log.LogInformation($"{nameof(Add)}({{{nameof(aggregateRoot)}}})", aggregateRoot);
            RegisterForTracking(aggregateRoot);
        }

        public void RegisterForTracking<TAggregate>(TAggregate aggregateRoot) where TAggregate : class, IOriginator, IEventProvider<TDomainEvent>, new()
        {
            _log.LogInformation($"{nameof(RegisterForTracking)}({{{nameof(aggregateRoot)}}})", aggregateRoot);
            _eventProviders.Add(aggregateRoot);
            _identityMap.Add(aggregateRoot);
        }

        public void Commit()
        {
            _log.LogInformation($"{nameof(Commit)}");
            _domainEventStorage.BeginTransaction();

            foreach (var eventProvider in _eventProviders)
            {
                _domainEventStorage.Save(eventProvider);
                _bus.Publish(eventProvider.GetChanges().Select(x => (object)x));
                eventProvider.Clear();
            }
            _eventProviders.Clear();

            _bus.CommitAsync();
            _domainEventStorage.Commit();
        }

        public void Rollback()
        {
            _log.LogInformation($"{nameof(Rollback)}");
            _bus.Rollback();
            _domainEventStorage.Rollback();
            foreach (var eventProvider in _eventProviders)
            {
                _identityMap.Remove(eventProvider.GetType(), eventProvider.Id);
            }
            _eventProviders.Clear();
        }

        private void LoadSnapShotIfExists(Guid id, IOriginator aggregateRoot)
        {
            _log.LogInformation($"{nameof(LoadSnapShotIfExists)}({{{nameof(id)}}}, {{{nameof(aggregateRoot)}}})", id, aggregateRoot);
            var snapShot = _domainEventStorage.GetSnapShot(id);
            if (snapShot == null)
                return;

            aggregateRoot.SetMemento(snapShot.Memento);
        }

        private void LoadRemainingHistoryEvents(Guid id, IEventProvider<TDomainEvent> aggregateRoot)
        {
            _log.LogInformation($"{nameof(LoadRemainingHistoryEvents)}({{{nameof(id)}}}, {{{nameof(aggregateRoot)}}})", id, aggregateRoot);
            var events = _domainEventStorage.GetEventsSinceLastSnapShot(id);
            if (events.Any())
            {
                aggregateRoot.LoadFromHistory(events);
                return;
            }

            aggregateRoot.LoadFromHistory(_domainEventStorage.GetAllEvents(id));
        }
    }
}