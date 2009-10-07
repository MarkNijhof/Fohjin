using System;

namespace Fohjin.DDD.Commands
{
    public class MakeADepositeOnAnAccountCommand : Command
    {
        public decimal Amount { get; private set; }

        public MakeADepositeOnAnAccountCommand(Guid id, decimal amount) : base(id)
        {
            Amount = amount;
        }
    }
}