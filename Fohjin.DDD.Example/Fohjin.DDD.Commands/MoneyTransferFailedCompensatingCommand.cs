namespace Fohjin.DDD.Commands
{
    [Serializable]
    public class MoneyTransferFailedCompensatingCommand : Command
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