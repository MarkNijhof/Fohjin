using System;

namespace Fohjin.DDD.Events.Client
{
    [Serializable]
    public class NewClientCreatedEvent : IDomainEvent
    {
        public Guid Id { get; private set; }
        int IDomainEvent.Version { get; set; }

        public Guid ClientId { get; private set; }
        public string ClientName { get; private set; }
        public string Street { get; private set; }
        public string StreetNumber { get; private set; }
        public string PostalCode { get; private set; }
        public string City { get; private set; }
        public string PhoneNumber { get; private set; }

        public NewClientCreatedEvent(Guid clientId, string cLientName, string street, string streetNumber, string postalCode, string city, string phoneNumber)
        {
            Id = Guid.NewGuid();
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