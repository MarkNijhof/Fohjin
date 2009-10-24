using System;

namespace Fohjin.DDD.Events.ActiveAccount
{
    [Serializable]
    public class MoneyTransferSendToAnOtherAccountEvent : DomainEvent 
    {
        public decimal Balance { get; private set; }
        public decimal Amount { get; private set; }
        public string SourceAccount { get; private set; }
        public string TargetAccount { get; private set; }

        public MoneyTransferSendToAnOtherAccountEvent(decimal balance, decimal amount, string sourceAccount, string targetAccount)
        {
            Balance = balance;
            Amount = amount;
            SourceAccount = sourceAccount;
            TargetAccount = targetAccount;
        }
    }
}