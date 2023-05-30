namespace Fohjin.DDD.Services.Models
{
    public class MoneyTransfer
    {
        public string SourceAccount { get; init; }
        public string TargetAccount { get; init; }
        public decimal Amount { get; init; }

        public MoneyTransfer(string sourceAccount, string targetAccount, decimal ammount)
        {
            SourceAccount = sourceAccount;
            TargetAccount = targetAccount;
            Amount = ammount;
        }
    }
}