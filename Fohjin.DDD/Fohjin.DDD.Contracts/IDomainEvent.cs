using System;
using Fohjin.DDD.Bus;
using Fohjin.DDD.EventStore;

namespace Fohjin.DDD.Contracts
{
    public interface IDomainEvent : IDomainEventBase, IMessage
    {
        Guid AggregateId { get; set; }
    }
}