using System;
using Fohjin.DDD.Commands;

namespace Fohjin.DDD.CommandHandlers
{
    public class ClientCreatedCommandHandler : ICommandHandler<ClientCreatedCommand>
    {
        public void Execute(ClientCreatedCommand command)
        {
            throw new NotImplementedException();
        }
    }
}