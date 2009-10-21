using System;
using Fohjin.DDD.Events.Client;

namespace Fohjin.DDD.EventHandlers
{
    public class ClientHasMovedEventHandler : IEventHandler<ClientHasMovedEvent>
    {
        public void Execute(ClientHasMovedEvent theEvent)
        {
            throw new NotImplementedException();
        }
    }
}