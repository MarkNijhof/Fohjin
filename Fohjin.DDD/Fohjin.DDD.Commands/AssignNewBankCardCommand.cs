using System;

namespace Fohjin.DDD.Commands
{
    [Serializable]
    public class AssignNewBankCardCommand : Command
    {
        public Guid AccountId { get; set; }

        public AssignNewBankCardCommand(Guid id, Guid accountId) : base(id)
        {
            AccountId = accountId;
        }
    }
}