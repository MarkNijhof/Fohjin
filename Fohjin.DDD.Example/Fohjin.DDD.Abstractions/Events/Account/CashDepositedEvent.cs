namespace Fohjin.DDD.Events.Account
{
    public record CashDepositedEvent : DomainEvent
    {
        public decimal Balance { get; init; }
        public decimal Amount { get; init; }

        public CashDepositedEvent(decimal balance, decimal amount)
        {
            Balance = balance;
            Amount = amount;
        }
    }
}