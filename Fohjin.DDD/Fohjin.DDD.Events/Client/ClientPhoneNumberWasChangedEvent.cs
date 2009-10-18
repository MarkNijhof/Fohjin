using System;

namespace Fohjin.DDD.Events.Client
{
    [Serializable]
    public class ClientPhoneNumberWasChangedEvent : DomainEvent
    {
        public Guid ClientId { get; private set; }
        public string PhoneNumber { get; private set; }

        public ClientPhoneNumberWasChangedEvent(Guid clientId, string phoneNumber)
        {
            ClientId = clientId;
            PhoneNumber = phoneNumber;
        }
    }
}