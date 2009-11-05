using System;

namespace Fohjin.DDD.EventStore
{
    public interface IUnitOfWork : IDisposable
    {
        void Complete();
        void Commit();
        void Rollback();
    }

    public interface IEventStoreUnitOfWork : IUnitOfWork
    {
        TAggregate GetById<TAggregate>(Guid id) where TAggregate : class, IOrginator, IEventProvider, new();
        void Add<TAggregate>(TAggregate aggregateRoot) where TAggregate : class, IOrginator, IEventProvider, new();
        void RegisterForTracking<TAggregate>(TAggregate aggregateRoot) where TAggregate : class, IOrginator, IEventProvider, new();
    }
}