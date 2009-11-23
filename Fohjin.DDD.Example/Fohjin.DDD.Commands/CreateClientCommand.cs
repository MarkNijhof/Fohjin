using System;

namespace Fohjin.DDD.Commands
{
    [Serializable]
    public class CreateClientCommand : Command
    {
        public string ClientName { get; private set; }
        public string Street { get; private set; }
        public string StreetNumber { get; private set; }
        public string PostalCode { get; private set; }
        public string City { get; private set; }
        public string PhoneNumber { get; private set; }

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