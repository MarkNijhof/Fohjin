using System;
using Fohjin.DDD.Domain.Mementos;

namespace Fohjin.DDD.EventStore
{
    public interface ISnapShot 
    {
        IMemento Memento { get; }
        Guid EventProviderId { get; }
        int Version { get; }
    }
}