using System;

namespace Fohjin.DDD.Commands
{
    public class CloseAnAccountCommand : Command
    {
        public CloseAnAccountCommand(Guid id) : base(id) { }
    }
}