using System;

namespace Fohjin.EventStore
{
    public interface IDomainEvent
    {
        Guid Id { get; }
        Guid AggregateId { get; set; }
        int Version { get; set; }
    }
}