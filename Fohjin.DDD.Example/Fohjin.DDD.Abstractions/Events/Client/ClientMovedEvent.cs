using System.Text.Json.Serialization;

namespace Fohjin.DDD.Events.Client
{
    public class ClientMovedEvent : DomainEvent
    {
        public string Street { get; set; } = null!;
        public string StreetNumber { get; set; } = null!;
        public string PostalCode { get; set; } = null!;
        public string City { get; set; } = null!;

        [JsonConstructor]
        public ClientMovedEvent() { }

        public ClientMovedEvent(string street, string streetNumber, string postalCode, string city)
        {
            Street = street;
            StreetNumber = streetNumber;
            PostalCode = postalCode;
            City = city;
        }
    }
}