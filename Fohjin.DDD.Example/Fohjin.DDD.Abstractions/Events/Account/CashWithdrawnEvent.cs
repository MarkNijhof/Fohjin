namespace Fohjin.DDD.Events.Account
{
    public class CashWithdrawnEvent : DomainEvent
    {
        public decimal Balance { get; set; }
        public decimal Amount { get; set; }

        public CashWithdrawnEvent(decimal balance, decimal amount)
        {
            Balance = balance;
            Amount = amount;
        }
    }
}