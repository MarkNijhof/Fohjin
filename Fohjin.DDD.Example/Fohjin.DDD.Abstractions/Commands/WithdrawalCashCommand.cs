namespace Fohjin.DDD.Commands
{
    public class WithdrawalCashCommand : Command
    {
        public decimal Amount { get; set; }

        public WithdrawalCashCommand(Guid id, decimal amount) : base(id)
        {
            Amount = amount;
        }
    }
}