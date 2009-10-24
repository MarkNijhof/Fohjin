using System;

namespace Fohjin.DDD.Commands
{
    [Serializable]
    public class WithdrawlCashCommand : Command
    {
        public decimal Amount { get; private set; }

        public WithdrawlCashCommand(Guid id, decimal amount) : base(id)
        {
            Amount = amount;
        }
    }
}