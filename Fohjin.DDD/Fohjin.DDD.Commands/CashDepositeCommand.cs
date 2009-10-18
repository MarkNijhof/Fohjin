using System;

namespace Fohjin.DDD.Commands
{
    [Serializable]
    public class CashDepositeCommand : Command
    {
        public decimal Amount { get; private set; }

        public CashDepositeCommand(Guid id, decimal amount) : base(id)
        {
            Amount = amount;
        }
    }
}