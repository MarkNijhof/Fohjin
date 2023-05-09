namespace Fohjin.DDD.Commands
{
    public record ReceiveMoneyTransferCommand : Command
    {
        public decimal Amount { get; init; }
        public string AccountNumber { get; init; }

        public ReceiveMoneyTransferCommand(Guid id, decimal amount, string accountNumber) : base(id)
        {
            Amount = amount;
            AccountNumber = accountNumber;
        }
    }
}