using Fohjin.DDD.EventStore.Storage.Memento;

namespace Fohjin.DDD.EventStore.Bus
{
    public interface IEventHandlerUnitOfWork : IUnitOfWork
    {
        void RegisterForPublishing<TAggregate>(TAggregate aggregateRoot) where TAggregate : class, IOrginator, IEventProvider, new();
    }
}