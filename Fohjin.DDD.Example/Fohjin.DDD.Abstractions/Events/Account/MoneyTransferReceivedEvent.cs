namespace Fohjin.DDD.Events.Account
{
    public class MoneyTransferReceivedEvent : DomainEvent
    {
        public decimal Balance { get; set; }
        public decimal Amount { get; set; }
        public string SourceAccount { get; set; }
        public string TargetAccount { get; set; }

        public MoneyTransferReceivedEvent(decimal balance, decimal amount, string sourceAccount, string targetAccount)
        {
            Balance = balance;
            Amount = amount;
            SourceAccount = sourceAccount;
            TargetAccount = targetAccount;
        }
    }
}