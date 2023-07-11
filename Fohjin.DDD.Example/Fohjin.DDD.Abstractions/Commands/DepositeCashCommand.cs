using System.Text.Json.Serialization;

namespace Fohjin.DDD.Commands
{
    public record DepositCashCommand : CommandBase
    {
        public decimal Amount { get; init; }

        [JsonConstructor]
        public DepositCashCommand() : base() { }
        public DepositCashCommand(Guid id, decimal amount) : base(id)
        {
            Amount = amount;
        }
    }
}