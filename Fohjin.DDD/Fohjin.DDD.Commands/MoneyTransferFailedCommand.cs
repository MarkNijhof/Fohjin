using System;

namespace Fohjin.DDD.Commands
{
    [Serializable]
    public class MoneyTransferFailedCommand : Command
    {
        public decimal Amount { get; private set; }
        public string AccountNumber { get; private set; }

        public MoneyTransferFailedCommand(Guid id, decimal amount, string targetAccountNumber) : base(id)
        {
            Amount = amount;
            AccountNumber = targetAccountNumber;
        }
    }
}