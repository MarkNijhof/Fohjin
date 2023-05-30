namespace Fohjin.DDD.Commands
{
    public class SendMoneyTransferCommand : Command
    {
        public decimal Amount { get; set; }
        public string AccountNumber { get; set; }

        public SendMoneyTransferCommand(Guid id, decimal amount, string accountNumber) : base(id)
        {
            Amount = amount;
            AccountNumber = accountNumber;
        }
    }
}