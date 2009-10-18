using System;

namespace Fohjin.DDD.Commands
{
    [Serializable]
    public class CashWithdrawlCommand : Command
    {
        public decimal Amount { get; private set; }

        public CashWithdrawlCommand(Guid id, decimal amount) : base(id)
        {
            Amount = amount;
        }
    }
}