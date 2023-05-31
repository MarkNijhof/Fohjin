using System.Text.Json.Serialization;

namespace Fohjin.DDD.Commands
{
    public class DepositCashCommand : CommandBase
    {
        public decimal Amount { get; set; }

        [JsonConstructor]
        public DepositCashCommand() : base() { }
        public DepositCashCommand(Guid id, decimal amount) : base(id)
        {
            Amount = amount;
        }
    }
}