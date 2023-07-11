using System.Text.Json.Serialization;

namespace Fohjin.DDD.Commands
{
    public record SendMoneyTransferCommand : CommandBase
    {
        public decimal Amount { get; init; }
        public string? AccountNumber { get; init; }


        [JsonConstructor]
        public SendMoneyTransferCommand() : base() { }
        public SendMoneyTransferCommand(Guid id, decimal amount, string? accountNumber) : base(id)
        {
            Amount = amount;
            AccountNumber = accountNumber;
        }
    }
}