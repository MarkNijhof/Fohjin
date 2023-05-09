namespace Fohjin.DDD.Events.Account
{
    public record MoneyTransferSendEvent : DomainEvent
    {
        public decimal Balance { get; init; }
        public decimal Amount { get; init; }
        public string SourceAccount { get; init; }
        public string TargetAccount { get; init; }

        public MoneyTransferSendEvent(decimal balance, decimal amount, string sourceAccount, string targetAccount)
        {
            Balance = balance;
            Amount = amount;
            SourceAccount = sourceAccount;
            TargetAccount = targetAccount;
        }
    }
}