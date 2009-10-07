using System;
using Fohjin.DDD.Bus;

namespace Fohjin.DDD.Commands
{
    public interface ICommand : IMessage
    {
        Guid Id { get; }
    }
}