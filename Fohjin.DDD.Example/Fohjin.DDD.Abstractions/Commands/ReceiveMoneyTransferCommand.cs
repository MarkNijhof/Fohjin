using System.Text.Json.Serialization;

namespace Fohjin.DDD.Commands
{
    public class ReceiveMoneyTransferCommand : CommandBase
    {
        public decimal Amount { get; set; }
        public string AccountNumber { get; set; }

        [JsonConstructor]
        public ReceiveMoneyTransferCommand() : base() { }

        public ReceiveMoneyTransferCommand(Guid id, decimal amount, string accountNumber) : base(id)
        {
            Amount = amount;
            AccountNumber = accountNumber;
        }
    }
}