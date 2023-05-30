using System.Text.Json.Serialization;

namespace Fohjin.DDD.Events.Client
{
    public class ClientCreatedEvent : DomainEvent
    {
        public Guid ClientId { get; set; }
        public string ClientName { get; set; } = null!;
        public string Street { get; set; } = null!;
        public string StreetNumber { get; set; } = null!;
        public string PostalCode { get; set; } = null!;
        public string City { get; set; } = null!;
        public string PhoneNumber { get; set; } = null!;

        [JsonConstructor]
        public ClientCreatedEvent() { }

        public ClientCreatedEvent(Guid clientId, string clientName, string street, string streetNumber, string postalCode, string city, string phoneNumber)
        {
            ClientId = clientId;
            ClientName = clientName;
            Street = street;
            StreetNumber = streetNumber;
            PostalCode = postalCode;
            City = city;
            PhoneNumber = phoneNumber;
        }
    }
}