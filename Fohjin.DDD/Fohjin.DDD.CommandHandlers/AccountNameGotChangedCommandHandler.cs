using System;
using Fohjin.DDD.Commands;

namespace Fohjin.DDD.CommandHandlers
{
    public class AccountNameGotChangedCommandHandler : ICommandHandler<AccountNameGotChangedCommand>
    {
        public void Execute(AccountNameGotChangedCommand command)
        {
            throw new NotImplementedException();
        }
    }
}