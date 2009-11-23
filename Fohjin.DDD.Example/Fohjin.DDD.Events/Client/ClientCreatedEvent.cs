using System;

namespace Fohjin.DDD.Events.Client
{
    [Serializable]
    public class ClientCreatedEvent : DomainEvent
    {
        public Guid ClientId { get; private set; }
        public string ClientName { get; private set; }
        public string Street { get; private set; }
        public string StreetNumber { get; private set; }
        public string PostalCode { get; private set; }
        public string City { get; private set; }
        public string PhoneNumber { get; private set; }

        public ClientCreatedEvent(Guid clientId, string cLientName, string street, string streetNumber, string postalCode, string city, string phoneNumber)
        {
            ClientId = clientId;
            ClientName = cLientName;
            Street = street;
            StreetNumber = streetNumber;
            PostalCode = postalCode;
            City = city;
            PhoneNumber = phoneNumber;
        }
    }
}