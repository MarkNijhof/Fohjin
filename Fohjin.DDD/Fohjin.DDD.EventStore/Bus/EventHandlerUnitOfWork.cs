using System.Collections.Generic;
using System.Linq;
using System.Transactions;
using Fohjin.DDD.EventStore.Storage.Memento;

namespace Fohjin.DDD.EventStore.Bus
{
    public class EventHandlerUnitOfWork : IEventHandlerUnitOfWork
    {
        private readonly IEventBus _eventBus;
        private readonly List<IEventProvider> _events;
        private CommittableTransaction _transactionScope;

        public EventHandlerUnitOfWork(IEventBus eventBus)
        {
            _eventBus = eventBus;
            _events = new List<IEventProvider>();
        }

        public void RegisterForPublishing<TAggregate>(TAggregate aggregateRoot) where TAggregate : class, IOrginator, IEventProvider, new()
        {
            _events.Add(aggregateRoot);
        }

        public void Complete()
        {
            _transactionScope = new CommittableTransaction();

            _eventBus.PublishMultiple(_events.SelectMany(x => x.GetChanges()).ToList());
            _events.Clear();
        }

        public void Commit()
        {
            _transactionScope.Commit();
            _transactionScope.Dispose();
        }

        public void Rollback()
        {
            _transactionScope.Rollback();
            _transactionScope.Dispose();
            _events.Clear();
        }
    }
}