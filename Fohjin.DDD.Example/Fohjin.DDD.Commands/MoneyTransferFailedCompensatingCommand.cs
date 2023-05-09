namespace Fohjin.DDD.Commands
{
    public record MoneyTransferFailedCompensatingCommand : Command
    {
        public decimal Amount { get; init; }
        public string AccountNumber { get; init; }

        public MoneyTransferFailedCompensatingCommand(Guid id, decimal amount, string targetAccountNumber) : base(id)
        {
            Amount = amount;
            AccountNumber = targetAccountNumber;
        }
    }
}