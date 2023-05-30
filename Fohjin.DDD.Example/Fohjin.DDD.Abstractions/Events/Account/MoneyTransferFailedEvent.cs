namespace Fohjin.DDD.Events.Account
{
    public class MoneyTransferFailedEvent : DomainEvent
    {
        public decimal Balance { get; set; }
        public decimal Amount { get; set; }
        public string TargetAccount { get; set; }

        public MoneyTransferFailedEvent(decimal balance, decimal amount, string targetAccount)
        {
            Balance = balance;
            Amount = amount;
            TargetAccount = targetAccount;
        }
    }
}