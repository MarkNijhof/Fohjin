using System.Text.Json.Serialization;

namespace Fohjin.DDD.Commands
{
    public record WithdrawalCashCommand : CommandBase
    {
        public decimal Amount { get; init; }


        [JsonConstructor]
        public WithdrawalCashCommand() : base() { }
        public WithdrawalCashCommand(Guid id, decimal amount) : base(id)
        {
            Amount = amount;
        }
    }
}