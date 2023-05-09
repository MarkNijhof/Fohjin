namespace Fohjin.DDD.Commands
{
    [Serializable]
    public class WithdrawCashCommand : Command
    {
        public decimal Amount { get; init; }

        public WithdrawCashCommand(Guid id, decimal amount) : base(id)
        {
            Amount = amount;
        }
    }
}