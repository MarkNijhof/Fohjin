using System;

namespace Fohjin.DDD.Commands
{
    [Serializable]
    public class SendMoneyTransferCommand : Command
    {
        public decimal Amount { get; private set; }
        public string AccountNumber { get; private set; }

        public SendMoneyTransferCommand(Guid id, decimal amount, string accountNumber) : base(id)
        {
            Amount = amount;
            AccountNumber = accountNumber;
        }
    }
}