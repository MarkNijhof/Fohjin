using System;

namespace Fohjin.DDD.Events.ActiveAccount
{
    [Serializable]
    public class DepositeEvent : DomainEvent
    {
        public decimal Balance { get; private set; }
        public decimal Amount { get; private set; }

        public DepositeEvent(decimal balance, decimal amount)
        {
            Balance = balance;
            Amount = amount;
        }
    }
}