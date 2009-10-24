using System;

namespace Fohjin.DDD.Commands
{
    [Serializable]
    public class DepositeCashCommand : Command
    {
        public decimal Amount { get; private set; }

        public DepositeCashCommand(Guid id, decimal amount) : base(id)
        {
            Amount = amount;
        }
    }
}