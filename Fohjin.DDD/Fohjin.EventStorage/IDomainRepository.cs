using System;
using Fohjin.DDD.Domain;
using Fohjin.DDD.Domain.Entities.Mementos;

namespace Fohjin.EventStorage
{
    public interface IDomainRepository 
    {
        TAggregate GetById<TAggregate>(Guid id) where TAggregate : IOrginator, IEventProvider, new();
        void Save<TAggregate>(TAggregate activeAccount) where TAggregate : IOrginator, IEventProvider, new();
    }
}