namespace Fohjin.DDD.Commands
{
    public class DepositCashCommand : Command
    {
        public decimal Amount { get; set; }

        public DepositCashCommand(Guid id, decimal amount) : base(id)
        {
            Amount = amount;
        }
    }
}