namespace Fohjin.DDD.Commands
{
    public class MoneyTransferFailedCompensatingCommand : Command
    {
        public decimal Amount { get; set; }
        public string AccountNumber { get; set; }

        public MoneyTransferFailedCompensatingCommand(Guid id, decimal amount, string targetAccountNumber) : base(id)
        {
            Amount = amount;
            AccountNumber = targetAccountNumber;
        }
    }
}