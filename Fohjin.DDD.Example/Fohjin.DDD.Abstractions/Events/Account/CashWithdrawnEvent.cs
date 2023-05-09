namespace Fohjin.DDD.Events.Account
{
    public record CashWithdrawnEvent : DomainEvent
    {
        public decimal Balance { get; init; }
        public decimal Amount { get; init; }

        public CashWithdrawnEvent(decimal balance, decimal amount)
        {
            Balance = balance;
            Amount = amount;
        }
    }
}