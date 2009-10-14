using System;

namespace Fohjin.DDD.Commands
{
    public class AccountNameGotChangedCommand : Command
    {
        public string AccountName { get; private set; }

        public AccountNameGotChangedCommand(Guid id, string accountName) : base(id)
        {
            AccountName = accountName;
        }
    }
}