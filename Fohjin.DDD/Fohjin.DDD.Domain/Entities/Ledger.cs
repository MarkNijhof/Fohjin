using Fohjin.DDD.Domain.ValueObjects;

namespace Fohjin.DDD.Domain.Entities
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
}