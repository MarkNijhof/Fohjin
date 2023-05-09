using Fohjin.DDD.EventStore;

namespace Fohjin.DDD.Events
{
    public record DomainEvent : IDomainEvent
    {
        public Guid Id { get; init; }
        public Guid AggregateId { get; set; }
        int IDomainEvent.Version { get; set; }

        public DomainEvent()
        {
            Id = Guid.NewGuid();
        }
    }
}