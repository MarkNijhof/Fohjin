using System;

namespace Fohjin.DDD.Commands
{
    [Serializable]
    public class AddNewAccountToClientCommand : Command
    {
        public string AccountName { get; private set; }

        public AddNewAccountToClientCommand(Guid id, string accountName) : base(id)
        {
            AccountName = accountName;
        }
    }
}