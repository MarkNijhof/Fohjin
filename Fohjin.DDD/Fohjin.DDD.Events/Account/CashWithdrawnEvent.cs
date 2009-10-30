using System;

namespace Fohjin.DDD.Events.Account
{
    [Serializable]
    public class CashWithdrawnEvent : DomainEvent
    {
        public decimal Balance { get; private set; }
        public decimal Amount { get; private set; }

        public CashWithdrawnEvent(decimal balance, decimal amount)
        {
            Balance = balance;
            Amount = amount;
        }
    }
}