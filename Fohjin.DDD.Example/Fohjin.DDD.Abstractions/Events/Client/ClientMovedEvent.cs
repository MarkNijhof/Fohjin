using System.Text.Json.Serialization;

namespace Fohjin.DDD.Events.Client
{
    public class ClientMovedEvent : DomainEvent
    {
        public string Street { get; set; }
        public string StreetNumber { get; set; }
        public string PostalCode { get; set; }
        public string City { get; set; }

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