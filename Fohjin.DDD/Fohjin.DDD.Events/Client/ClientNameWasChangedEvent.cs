using System;

namespace Fohjin.DDD.Events.Client
{
    [Serializable]
    public class ClientNameWasChangedEvent : DomainEvent
    {
        public Guid ClientId { get; private set; }
        public string ClientName { get; private set; }

        public ClientNameWasChangedEvent(Guid clientId, string cLientName)
        {
            ClientId = clientId;
            ClientName = cLientName;
        }
    }
}