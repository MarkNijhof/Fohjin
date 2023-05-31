using System.Text.Json.Serialization;

namespace Fohjin.DDD.Commands
{
    public class ClientIsMovingCommand : CommandBase
    {
        public string Street { get; set; }
        public string StreetNumber { get; set; }
        public string PostalCode { get; set; }
        public string City { get; set; }


        [JsonConstructor]
        public ClientIsMovingCommand() : base() { }
        public ClientIsMovingCommand(Guid id, string street, string streetNumber, string postalCode, string city) : base(id)
        {
            Street = street;
            StreetNumber = streetNumber;
            PostalCode = postalCode;
            City = city;
        }
    }
}