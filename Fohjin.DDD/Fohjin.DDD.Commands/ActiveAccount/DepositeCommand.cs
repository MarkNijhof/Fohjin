using System;

namespace Fohjin.DDD.Commands.ActiveAccount
{
    public class DepositeCommand : Command
    {
        public DepositeCommand(Guid id) : base(id) {}
    }
}