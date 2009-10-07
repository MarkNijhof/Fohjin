using System;

namespace Fohjin.DDD.Commands
{
    public class AddNewAccountToClientCommand : Command
    {
        public Guid AccountId { get; private set; }
        public string AccountName { get; private set; }

        public AddNewAccountToClientCommand(Guid id, Guid accountId, string accountName) : base(id)
        {
            AccountId = accountId;
            AccountName = accountName;
        }
    }
}