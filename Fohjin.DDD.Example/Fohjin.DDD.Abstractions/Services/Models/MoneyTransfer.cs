namespace Fohjin.DDD.Services.Models
{
    public record MoneyTransfer
    {
        public string? SourceAccount { get; init; }
        public string? TargetAccount { get; init; }
        public decimal Amount { get; init; }

        public MoneyTransfer(string? sourceAccount, string? targetAccount, decimal ammount)
        {
            SourceAccount = sourceAccount;
            TargetAccount = targetAccount;
            Amount = ammount;
        }
    }
}