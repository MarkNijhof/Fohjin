using System;
using Fohjin.DDD.EventStore.Storage.Memento;

namespace Fohjin.DDD.EventStore.Storage
{
    public interface IIdentityMap<TDomainEvent> where TDomainEvent : IDomainEvent
    {
        TAggregate GetById<TAggregate>(Guid id) where TAggregate : class, IOrginator, IEventProvider<TDomainEvent>, new();
        void Add<TAggregate>(TAggregate aggregateRoot) where TAggregate : class, IOrginator, IEventProvider<TDomainEvent>, new();
        void Remove(Type aggregateRootType, Guid aggregateRootId);
    }
}