using System;

namespace Fohjin.EventStore
{
    public interface IDomainEvent
    {
        Guid EventId { get; }
        Guid AggregateId { get; set; }
        int EventVersion { get; set; }
    }
}