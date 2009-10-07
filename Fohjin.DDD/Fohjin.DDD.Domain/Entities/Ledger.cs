using Fohjin.DDD.Domain.ValueObjects;

namespace Fohjin.DDD.Domain.Entities
{
    public class Ledger
    {
        private readonly Amount _amount;

        public Ledger(Amount amount)
        {
            _amount = amount;
        }

        public static implicit operator decimal(Ledger ledger)
        {
            return ledger._amount;
        }

        public static implicit operator Ledger(decimal amount)
        {
            return new Ledger(amount);
        }
    }

    public class CreditMutation : Ledger
    {
        public CreditMutation(Amount amount) : base(amount) {}
    }

    public class DebitMutation : Ledger
    {
        public DebitMutation(Amount amount) : base(amount) {}
    }
}