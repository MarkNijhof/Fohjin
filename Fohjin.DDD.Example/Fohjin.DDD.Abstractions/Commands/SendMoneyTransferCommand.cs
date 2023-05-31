using System.Text.Json.Serialization;

namespace Fohjin.DDD.Commands
{
    public class SendMoneyTransferCommand : CommandBase
    {
        public decimal Amount { get; set; }
        public string AccountNumber { get; set; }


        [JsonConstructor]
        public SendMoneyTransferCommand() : base() { }
        public SendMoneyTransferCommand(Guid id, decimal amount, string accountNumber) : base(id)
        {
            Amount = amount;
            AccountNumber = accountNumber;
        }
    }
}