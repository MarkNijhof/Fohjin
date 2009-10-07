using System;
using Fohjin.DDD.Domain.Entities.Mementos;

namespace Fohjin.EventStorage
{
    public interface ISnapShot 
    {
        IMemento Memento { get; }
        Guid EventProviderId { get; }
        int Version { get; }
    }
}