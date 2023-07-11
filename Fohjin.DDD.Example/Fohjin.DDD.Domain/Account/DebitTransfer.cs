namespace Fohjin.DDD.Domain.Account
{
    public class DebitTransfer : Ledger
    {
        public DebitTransfer(Amount amount, AccountNumber account) : base(amount, account) { }
    }
}