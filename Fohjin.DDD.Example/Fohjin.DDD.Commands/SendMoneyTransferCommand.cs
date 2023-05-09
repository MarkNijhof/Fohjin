namespace Fohjin.DDD.Commands
{
    [Serializable]
    public class SendMoneyTransferCommand : Command
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