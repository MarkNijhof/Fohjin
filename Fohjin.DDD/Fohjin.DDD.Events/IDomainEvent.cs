using System;

namespace Fohjin.DDD.Events
{
    public interface IDomainEvent
    {
        Guid Id { get; }
        int Version { get; set; }
    }
}