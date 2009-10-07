using System;

namespace Fohjin.DDD.Commands
{
    public class Command : ICommand
    {
        public Guid Id { get; private set; }

        public Command(Guid id)
        {
            Id = id;
        }
    }
}