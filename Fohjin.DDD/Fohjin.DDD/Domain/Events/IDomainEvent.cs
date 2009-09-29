
using System;

namespace Fohjin.DDD.Domain.Events
{
    public interface IDomainEvent
    {
        Guid Id { get; }
        DateTime TimeStamp { get; }
    }
}