using System;
using Fohjin.DDD.Bus;
using Fohjin.DDD.EventStore;

namespace Fohjin.DDD.Events
{
    [Serializable]
    public class DomainEvent : IDomainEvent, IMessage
    {
        public Guid Id { get; private set; }
        public Guid AggregateId { get; set; }
        int IDomainEvent.Version { get; set; }

        public DomainEvent()
        {
            Id = Guid.NewGuid();
        }
    }
}