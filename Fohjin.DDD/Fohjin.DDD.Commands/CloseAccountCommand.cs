using System;

namespace Fohjin.DDD.Commands
{
    public class CloseAccountCommand : Command
    {
        public CloseAccountCommand(Guid id) : base(id) { }
    }
}