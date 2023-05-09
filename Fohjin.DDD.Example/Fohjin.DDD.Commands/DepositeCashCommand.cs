namespace Fohjin.DDD.Commands
{
    [Serializable]
    public class DepositeCashCommand : Command
    {
        public decimal Amount { get; init; }

        public DepositeCashCommand(Guid id, decimal amount) : base(id)
        {
            Amount = amount;
        }
    }
}