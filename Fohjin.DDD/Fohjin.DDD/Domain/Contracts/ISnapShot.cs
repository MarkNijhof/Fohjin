using System;
using Fohjin.DDD.Domain.Memento;

namespace Fohjin.DDD.Domain.Contracts
{
    public interface ISnapShot 
    {
        IMemento Memento { get; }
        Guid EventLocation { get; }
    }
}