using System;

namespace Fohjin.DDD.Events.ActiveAccount
{
    [Serializable]
    public class MoneyTransferedToAnOtherAccountEvent : DomainEvent 
    {
        public decimal Amount { get; private set; }
        public decimal Balance { get; private set; }
        public string OtherAccount { get; private set; }

        public MoneyTransferedToAnOtherAccountEvent(decimal amount, decimal balance, string otherAccount)
        {
            Amount = amount;
            Balance = balance;
            OtherAccount = otherAccount;
        }
    }
}