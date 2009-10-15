using System;
using Fohjin.DDD.Commands;

namespace Fohjin.DDD.CommandHandlers
{
    public class ClientChangedTheirNameCommandHandler : ICommandHandler<ClientChangedTheirNameCommand>
    {
        public void Execute(ClientChangedTheirNameCommand command)
        {
            throw new NotImplementedException();
        }
    }
}