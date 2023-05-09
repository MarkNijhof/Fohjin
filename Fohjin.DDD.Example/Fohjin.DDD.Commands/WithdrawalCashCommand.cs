namespace Fohjin.DDD.Commands
{
    [Serializable]
    public class WithdrawalCashCommand : Command
    {
        public decimal Amount { get; init; }

        public WithdrawalCashCommand(Guid id, decimal amount) : base(id)
        {
            Amount = amount;
        }
    }
}