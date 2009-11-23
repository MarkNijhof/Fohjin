namespace Fohjin.DDD.Services
{
    public class MoneyTransfer
    {
        public string SourceAccount { get; private set; }
        public string TargetAccount { get; private set; }
        public decimal Ammount { get; private set; }

        public MoneyTransfer(string sourceAccount, string targetAccount, decimal ammount)
        {
            SourceAccount = sourceAccount;
            TargetAccount = targetAccount;
            Ammount = ammount;
        }
    }
}