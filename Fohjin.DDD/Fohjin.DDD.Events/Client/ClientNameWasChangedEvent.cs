using System;

namespace Fohjin.DDD.Events.Client
{
    [Serializable]
    public class ClientNameWasChangedEvent : DomainEvent
    {
        public string ClientName { get; private set; }

        public ClientNameWasChangedEvent(string cLientName)
        {
            ClientName = cLientName;
        }
    }
}