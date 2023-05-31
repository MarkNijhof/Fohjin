using System.Text.Json.Serialization;

namespace Fohjin.DDD.Domain.Client
{
    public class Address
    {
        public string Street { get; set; }
        public string StreetNumber { get; set; }
        public string PostalCode { get; set; }
        public string City { get; set; }

        [JsonConstructor]
        public Address() { }
        public Address(string street, string streetNumber, string postalCode, string city)
        {
            Street = street;
            StreetNumber = streetNumber;
            PostalCode = postalCode;
            City = city;
        }
    }
}