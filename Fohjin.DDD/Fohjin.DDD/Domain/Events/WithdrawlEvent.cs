using Fohjin.DDD.Domain.ValueObjects;

namespace Fohjin.DDD.Domain.Events
{
    public class WithdrawlEvent : IDomainEvent
    {
        public WithdrawlEvent(Balance balance, Amount amount)
        {
            Balance = balance;
            Amount = amount;
        }

        public Balance Balance { get; private set; }
        public Amount Amount { get; private set; }
    }
}