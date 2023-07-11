namespace Fohjin.DDD.Domain.Account
{
    public class CreditTransfer : Ledger
    {
        public CreditTransfer(Amount amount, AccountNumber account) : base(amount, account) { }
    }
}