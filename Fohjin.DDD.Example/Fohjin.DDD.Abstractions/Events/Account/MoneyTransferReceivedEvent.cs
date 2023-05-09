namespace Fohjin.DDD.Events.Account
{
    public record MoneyTransferReceivedEvent : DomainEvent
    {
        public decimal Balance { get; init; }
        public decimal Amount { get; init; }
        public string SourceAccount { get; set; }
        public string TargetAccount { get; init; }

        public MoneyTransferReceivedEvent(decimal balance, decimal amount, string sourceAccount, string targetAccount)
        {
            Balance = balance;
            Amount = amount;
            SourceAccount = sourceAccount;
            TargetAccount = targetAccount;
        }
    }
}