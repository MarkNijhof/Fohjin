using System;

namespace Fohjin.DDD.Events.ActiveAccount
{
    [Serializable]
    public class MoneyTransferedFromAnOtherAccountEvent : DomainEvent 
    {
        public decimal Balance { get; private set; }
        public decimal Amount { get; private set; }
        public string OtherAccount { get; private set; }

        public MoneyTransferedFromAnOtherAccountEvent(decimal balance, decimal amount, string otherAccount)
        {
            Balance = balance;
            Amount = amount;
            OtherAccount = otherAccount;
        }
    }
}