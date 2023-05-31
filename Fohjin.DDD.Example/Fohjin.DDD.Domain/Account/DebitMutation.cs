namespace Fohjin.DDD.Domain.Account
{
    public class DebitMutation : Ledger
    {
        public DebitMutation(Amount amount, AccountNumber account) : base(amount, account) { }
    }
}