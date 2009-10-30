namespace Fohjin.DDD.Domain.Account
{
    public abstract class Ledger
    {
        public Amount Amount { get; private set; }
        public AccountNumber Account { get; private set; }

        protected Ledger(Amount amount, AccountNumber account)
        {
            Amount = amount;
            Account = account;
        }

        public override string ToString()
        {
            return string.Format("{0} - {1} - {2}", GetType().Name, Account.Number, (decimal)Amount);
        }
    }

    public class CreditMutation : Ledger
    {
        public CreditMutation(Amount amount, AccountNumber account) : base(amount, account) { }
    }

    public class DebitMutation : Ledger
    {
        public DebitMutation(Amount amount, AccountNumber account) : base(amount, account) { }
    }

    public class CreditTransfer : Ledger
    {
        public CreditTransfer(Amount amount, AccountNumber account) : base(amount, account) { }
    }

    public class DebitTransfer : Ledger
    {
        public DebitTransfer(Amount amount, AccountNumber account) : base(amount, account) { }
    }

    public class DebitTransferFailed : Ledger
    {
        public DebitTransferFailed(Amount amount, AccountNumber account) : base(amount, account) { }
    }
}