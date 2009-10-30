using System;

namespace Fohjin.DDD.Events.Account
{
    [Serializable]
    public class MoneyTransferReceivedEvent : DomainEvent 
    {
        public decimal Balance { get; private set; }
        public decimal Amount { get; private set; }
        public string SourceAccount { get; set; }
        public string TargetAccount { get; private set; }

        public MoneyTransferReceivedEvent(decimal balance, decimal amount, string sourceAccount, string targetAccount)
        {
            Balance = balance;
            Amount = amount;
            SourceAccount = sourceAccount;
            TargetAccount = targetAccount;
        }
    }
}