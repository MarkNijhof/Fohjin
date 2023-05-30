namespace Fohjin.DDD.Services.Models
{
    public class MoneyTransfer
    {
        public string SourceAccount { get; set; }
        public string TargetAccount { get; set; }
        public decimal Amount { get; set; }

        public MoneyTransfer(string sourceAccount, string targetAccount, decimal ammount)
        {
            SourceAccount = sourceAccount;
            TargetAccount = targetAccount;
            Amount = ammount;
        }
    }
}