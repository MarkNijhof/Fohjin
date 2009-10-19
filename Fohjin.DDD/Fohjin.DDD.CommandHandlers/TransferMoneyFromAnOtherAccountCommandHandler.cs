using System;
using Fohjin.DDD.Commands;

namespace Fohjin.DDD.CommandHandlers
{
    public class TransferMoneyFromAnOtherAccountCommandHandler : ICommandHandler<TransferMoneyFromAnOtherAccountCommand>
    {
        public void Execute(TransferMoneyFromAnOtherAccountCommand command)
        {
            throw new NotImplementedException();
        }
    }
}