using System;
using Fohjin.DDD.Commands;

namespace Fohjin.DDD.CommandHandlers
{
    public class TransferMoneyToAnOtherAccountCommandHandler : ICommandHandler<TransferMoneyToAnOtherAccountCommand>
    {
        public void Execute(TransferMoneyToAnOtherAccountCommand command)
        {
            throw new NotImplementedException();
        }
    }
}