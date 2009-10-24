using System;

namespace Fohjin.DDD.Events.Client
{
    [Serializable]
    public class ClientPhoneNumberChangedEvent : DomainEvent
    {
        public string PhoneNumber { get; private set; }

        public ClientPhoneNumberChangedEvent(string phoneNumber)
        {
            PhoneNumber = phoneNumber;
        }
    }
}