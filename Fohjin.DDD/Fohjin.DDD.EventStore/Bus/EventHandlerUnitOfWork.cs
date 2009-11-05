using System;
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
        private TransactionScope _transactionScope;

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
            //_transactionScope = new TransactionScope();

            _eventBus.PublishMultiple(_events.SelectMany(x => x.GetChanges()).ToList());
            _events.Clear();
            //try
            //{
            //    _eventBus.PublishMultiple(_events);
            //    _events.Clear();
            //}
            //catch (Exception Ex)
            //{
            //    Rollback();
            //    throw;
            //}
        }

        public void Commit()
        {
            //_transactionScope.Complete();
        }

        public void Rollback()
        {
            //_transactionScope.Dispose();
            _events.Clear();
        }
    }
}