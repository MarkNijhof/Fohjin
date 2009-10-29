using System;
using Fohjin.DDD.Domain;
using Fohjin.DDD.Domain.Entities.Mementos;

namespace Fohjin.DDD.Contracts
{
    public interface IDomainRepository 
    {
        TAggregate GetById<TAggregate>(Guid id) where TAggregate : IOrginator, IEventProvider, new();
        void Save<TAggregate>(TAggregate aggregateRoot) where TAggregate : IOrginator, IEventProvider, new();
    }
}