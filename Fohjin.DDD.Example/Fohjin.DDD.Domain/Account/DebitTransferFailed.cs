namespace Fohjin.DDD.Domain.Account
{
    public class DebitTransferFailed : Ledger
    {
        public DebitTransferFailed(Amount amount, AccountNumber account) : base(amount, account) { }
    }
}