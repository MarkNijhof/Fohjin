using System.Text.Json.Serialization;

namespace Fohjin.DDD.Commands
{
    public class CreateClientCommand : CommandBase
    {
        public string ClientName { get; set; }
        public string Street { get; set; }
        public string StreetNumber { get; set; }
        public string PostalCode { get; set; }
        public string City { get; set; }
        public string PhoneNumber { get; set; }

        [JsonConstructor]
        public CreateClientCommand() : base() { }
        public CreateClientCommand(Guid id, string clientName, string street, string streetNumber, string postalCode, string city, string phoneNumber) : base(id)
        {
            ClientName = clientName;
            Street = street;
            StreetNumber = streetNumber;
            PostalCode = postalCode;
            City = city;
            PhoneNumber = phoneNumber;
        }
    }
}