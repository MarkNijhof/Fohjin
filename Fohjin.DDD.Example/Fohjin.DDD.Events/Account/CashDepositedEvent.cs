namespace Fohjin.DDD.Events.Account
{
    [Serializable]
    public class CashDepositdEvent : DomainEvent
    {
        public decimal Balance { get; private set; }
        public decimal Amount { get; private set; }

        public CashDepositdEvent(decimal balance, decimal amount)
        {
            Balance = balance;
            Amount = amount;
        }
    }
}