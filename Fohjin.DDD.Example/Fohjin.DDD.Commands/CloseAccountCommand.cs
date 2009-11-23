using System;

namespace Fohjin.DDD.Commands
{
    [Serializable]
    public class CloseAccountCommand : Command
    {
        public CloseAccountCommand(Guid id) : base(id) { }
    }
}