using System;

namespace Fohjin.DDD.Domain.Entities
{
    public interface IDomainAggregate
    {
        Guid Id { get;} 
    }
}