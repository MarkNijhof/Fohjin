using System;

namespace Fohjin.DDD.Events.Client
{
    [Serializable]
    public class ClientPhoneNumberWasChangedEvent : DomainEvent
    {
        public string PhoneNumber { get; private set; }

        public ClientPhoneNumberWasChangedEvent(string phoneNumber)
        {
            PhoneNumber = phoneNumber;
        }
    }
}