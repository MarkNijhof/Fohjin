using System;

namespace Fohjin.DDD.Events.Client
{
    [Serializable]
    public class ClientNameWasChangedEvent : IDomainEvent
    {
        public Guid Id { get; private set; }
        int IDomainEvent.Version { get; set; }

        public Guid ClientId { get; private set; }
        public string ClientName { get; private set; }

        public ClientNameWasChangedEvent(Guid clientId, string cLientName)
        {
            Id = Guid.NewGuid();
            ClientId = clientId;
            ClientName = cLientName;
        }
    }
}