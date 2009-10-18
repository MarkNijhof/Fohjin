using System;

namespace Fohjin.DDD.Events.ActiveAccount
{
    [Serializable]
    public class WithdrawlEvent : DomainEvent
    {
        public decimal Balance { get; private set; }
        public decimal Amount { get; private set; }

        public WithdrawlEvent(decimal balance, decimal amount)
        {
            Balance = balance;
            Amount = amount;
        }
    }
}