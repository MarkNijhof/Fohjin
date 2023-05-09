namespace Fohjin.DDD.Commands
{
    public record DepositCashCommand : Command
    {
        public decimal Amount { get; init; }

        public DepositCashCommand(Guid id, decimal amount) : base(id)
        {
            Amount = amount;
        }
    }
}