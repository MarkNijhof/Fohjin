namespace Fohjin.DDD.Domain.Account
{
    public class CreditMutation : Ledger
    {
        public CreditMutation(Amount amount, AccountNumber account) : base(amount, account) { }
    }
}