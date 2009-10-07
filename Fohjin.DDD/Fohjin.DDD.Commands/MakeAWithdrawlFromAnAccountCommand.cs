using System;

namespace Fohjin.DDD.Commands
{
    public class MakeAWithdrawlFromAnAccountCommand : Command
    {
        public decimal Amount { get; private set; }

        public MakeAWithdrawlFromAnAccountCommand(Guid id, decimal amount) : base(id)
        {
            Amount = amount;
        }
    }
}