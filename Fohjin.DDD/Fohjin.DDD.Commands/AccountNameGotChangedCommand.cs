using System;

namespace Fohjin.DDD.Commands
{
    [Serializable]
    public class AccountNameGotChangedCommand : Command
    {
        public string AccountName { get; private set; }

        public AccountNameGotChangedCommand(Guid id, string accountName) : base(id)
        {
            AccountName = accountName;
        }
    }
}