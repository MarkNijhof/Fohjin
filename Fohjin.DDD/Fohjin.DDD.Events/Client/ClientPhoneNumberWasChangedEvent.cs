using System;

namespace Fohjin.DDD.Events.Client
{
    [Serializable]
    public class ClientPhoneNumberWasChangedEvent : IDomainEvent
    {
        public Guid Id { get; private set; }
        int IDomainEvent.Version { get; set; }

        public Guid ClientId { get; private set; }
        public string PhoneNumber { get; private set; }

        public ClientPhoneNumberWasChangedEvent(Guid clientId, string phoneNumber)
        {
            Id = Guid.NewGuid();
            ClientId = clientId;
            PhoneNumber = phoneNumber;
        }
    }
}