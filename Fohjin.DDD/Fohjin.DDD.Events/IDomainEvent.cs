using System;
using Fohjin.DDD.Bus;

namespace Fohjin.DDD.Events
{
    public interface IDomainEvent : IMessage
    {
        Guid Id { get; }
        int Version { get; set; }
    }
}