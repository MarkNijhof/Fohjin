using System.Text.Json.Serialization;

namespace Fohjin.DDD.Commands
{
    public class WithdrawalCashCommand : CommandBase
    {
        public decimal Amount { get; set; }


        [JsonConstructor]
        public WithdrawalCashCommand() : base() { }
        public WithdrawalCashCommand(Guid id, decimal amount) : base(id)
        {
            Amount = amount;
        }
    }
}