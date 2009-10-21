using System;
using Fohjin.DDD.Bus;

namespace Fohjin.DDD.Events
{
    public interface IDomainEvent : IMessage
    {
        Guid Id { get; }
        Guid EntityId { get; set; }
        int Version { get; set; }
    }
}