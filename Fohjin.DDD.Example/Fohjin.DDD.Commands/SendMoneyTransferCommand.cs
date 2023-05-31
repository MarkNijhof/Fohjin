namespace Fohjin.DDD.Commands
{
    public record SendMoneyTransferCommand : Command
    {
        public decimal Amount { get; init; }
        public string AccountNumber { get; init; }

        public SendMoneyTransferCommand(Guid id, decimal amount, string accountNumber) : base(id)
        {
            Amount = amount;
            AccountNumber = accountNumber;
        }
    }
}