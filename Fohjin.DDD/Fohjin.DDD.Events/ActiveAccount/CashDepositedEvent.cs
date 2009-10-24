using System;

namespace Fohjin.DDD.Events.ActiveAccount
{
    [Serializable]
    public class CashDepositedEvent : DomainEvent
    {
        public decimal Balance { get; private set; }
        public decimal Amount { get; private set; }

        public CashDepositedEvent(decimal balance, decimal amount)
        {
            Balance = balance;
            Amount = amount;
        }
    }
}