using System;

namespace Fohjin.DDD.Commands
{
    public interface ICommand
    {
        Guid Id { get; }
    }
}