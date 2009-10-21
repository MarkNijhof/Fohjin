using System;
using Fohjin.DDD.Events.Client;

namespace Fohjin.DDD.EventHandlers
{
    public class AccountWasAssignedToClientEventHandler : IEventHandler<AccountWasAssignedToClientEvent>
    {
        public void Execute(AccountWasAssignedToClientEvent theEvent)
        {
            throw new NotImplementedException();
        }
    }
}