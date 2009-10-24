using System;

namespace Fohjin.DDD.Events.Client
{
    [Serializable]
    public class ClientNameChangedEvent : DomainEvent
    {
        public string ClientName { get; private set; }

        public ClientNameChangedEvent(string cLientName)
        {
            ClientName = cLientName;
        }
    }
}