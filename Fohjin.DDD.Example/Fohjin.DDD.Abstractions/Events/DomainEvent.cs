using Fohjin.DDD.EventStore;

namespace Fohjin.DDD.Events;

public abstract record DomainEvent : IDomainEvent
{
    public Guid Id { get; init; }
    public Guid AggregateId { get; set; }
    public int Version { get; set; }

    public DomainEvent()
    {
        Id = Guid.NewGuid();
    }
}