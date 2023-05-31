namespace Fohjin.DDD.Commands
{
    public record WithdrawalCashCommand : Command
    {
        public decimal Amount { get; init; }

        public WithdrawalCashCommand(Guid id, decimal amount) : base(id)
        {
            Amount = amount;
        }
    }
}