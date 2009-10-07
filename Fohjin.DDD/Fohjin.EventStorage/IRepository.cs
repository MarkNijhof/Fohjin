using System;
using Fohjin.DDD.Domain.Entities.Mementos;

namespace Fohjin.EventStorage
{
    public interface IRepository<TAggregate> where TAggregate : IOrginator, new() 
    {
        TAggregate GetById(Guid id);
        void Save(TAggregate activeAccount);
    }
}