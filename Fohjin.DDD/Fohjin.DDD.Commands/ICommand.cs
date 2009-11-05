using System;
using Fohjin.DDD.EventStore;

namespace Fohjin.DDD.Commands
{
    public interface ICommand : IMessage
    {
        Guid Id { get; }
    }
}