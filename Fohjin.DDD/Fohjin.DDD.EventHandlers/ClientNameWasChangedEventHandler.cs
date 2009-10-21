using System;
using Fohjin.DDD.Events.Client;

namespace Fohjin.DDD.EventHandlers
{
    public class ClientNameWasChangedEventHandler : IEventHandler<ClientNameWasChangedEvent>
    {
        public void Execute(ClientNameWasChangedEvent theEvent)
        {
            throw new NotImplementedException();
        }
    }
}