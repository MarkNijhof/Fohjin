using System.Text.Json.Serialization;

namespace Fohjin.DDD.Commands
{
    public class MoneyTransferFailedCompensatingCommand : CommandBase
    {
        public decimal Amount { get; set; }
        public string AccountNumber { get; set; }

        [JsonConstructor]
        public MoneyTransferFailedCompensatingCommand() : base() { }
        public MoneyTransferFailedCompensatingCommand(Guid id, decimal amount, string targetAccountNumber) : base(id)
        {
            Amount = amount;
            AccountNumber = targetAccountNumber;
        }
    }
}