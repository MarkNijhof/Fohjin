namespace Fohjin.DDD.Commands
{
    public class ReceiveMoneyTransferCommand : Command
    {
        public decimal Amount { get; set; }
        public string AccountNumber { get; set; }

        public ReceiveMoneyTransferCommand(Guid id, decimal amount, string accountNumber) : base(id)
        {
            Amount = amount;
            AccountNumber = accountNumber;
        }
    }
}