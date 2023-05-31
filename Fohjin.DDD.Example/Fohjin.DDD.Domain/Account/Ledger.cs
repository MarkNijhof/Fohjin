namespace Fohjin.DDD.Domain.Account
{
    public abstract class Ledger
    {
        public Amount Amount { get; init; }
        public AccountNumber Account { get; init; }

        protected Ledger(Amount amount, AccountNumber account)
        {
            Amount = amount;
            Account = account;
        }

        public override string ToString() =>
            string.Join(" - ", GetType().Name, Account.Number, (decimal)Amount);
    }
}