namespace Fohjin.DDD.Events.Account
{
    public class CashDepositedEvent : DomainEvent
    {
        public decimal Balance { get; set; }
        public decimal Amount { get; set; }

        public CashDepositedEvent(decimal balance, decimal amount)
        {
            Balance = balance;
            Amount = amount;
        }
    }
}