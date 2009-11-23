using System;

namespace Fohjin.DDD.Commands
{
    [Serializable]
    public class ChangeAccountNameCommand : Command
    {
        public string AccountName { get; private set; }

        public ChangeAccountNameCommand(Guid id, string accountName) : base(id)
        {
            AccountName = accountName;
        }
    }
}