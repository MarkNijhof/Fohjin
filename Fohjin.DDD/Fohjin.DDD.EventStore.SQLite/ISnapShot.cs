using System;
using Fohjin.DDD.Domain.Entities.Mementos;

namespace Fohjin.DDD.EventStore.SQLite
{
    public interface ISnapShot 
    {
        IMemento Memento { get; }
        Guid EventProviderId { get; }
        int Version { get; }
    }
}