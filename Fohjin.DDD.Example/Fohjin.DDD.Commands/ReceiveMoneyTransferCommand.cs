namespace Fohjin.DDD.Commands
{
    [Serializable]
    public class ReceiveMoneyTransferCommand : Command
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