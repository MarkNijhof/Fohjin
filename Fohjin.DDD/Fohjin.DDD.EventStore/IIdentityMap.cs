using System;

namespace Fohjin.DDD.EventStore
{
    public interface IIdentityMap
    {
        TAggregate GetById<TAggregate>(Guid id) where TAggregate : class, IOrginator, IEventProvider, new();
        void Add<TAggregate>(TAggregate aggregateRoot) where TAggregate : class, IOrginator, IEventProvider, new();
        void Remove(Type aggregateRootType, Guid aggregateRootId);
    }
}