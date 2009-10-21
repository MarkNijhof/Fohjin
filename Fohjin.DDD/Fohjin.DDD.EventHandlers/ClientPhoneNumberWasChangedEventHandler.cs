using System;
using Fohjin.DDD.Events.Client;

namespace Fohjin.DDD.EventHandlers
{
    public class ClientPhoneNumberWasChangedEventHandler : IEventHandler<ClientPhoneNumberWasChangedEvent>
    {
        public void Execute(ClientPhoneNumberWasChangedEvent theEvent)
        {
            throw new NotImplementedException();
        }
    }
}