using System.Text.Json.Serialization;

namespace Fohjin.DDD.Commands
{
    public record ReceiveMoneyTransferCommand : CommandBase
    {
        public decimal Amount { get; init; }
        public string? AccountNumber { get; init; }

        [JsonConstructor]
        public ReceiveMoneyTransferCommand() : base() { }

        public ReceiveMoneyTransferCommand(Guid id, decimal amount, string? accountNumber) : base(id)
        {
            Amount = amount;
            AccountNumber = accountNumber;
        }
    }
}