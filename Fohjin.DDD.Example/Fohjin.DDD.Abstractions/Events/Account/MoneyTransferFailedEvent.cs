namespace Fohjin.DDD.Events.Account
{
    public record MoneyTransferFailedEvent : DomainEvent
    {
        public decimal Balance { get; init; }
        public decimal Amount { get; init; }
        public string TargetAccount { get; init; }

        public MoneyTransferFailedEvent(decimal balance, decimal amount, string targetAccount)
        {
            Balance = balance;
            Amount = amount;
            TargetAccount = targetAccount;
        }
    }
}