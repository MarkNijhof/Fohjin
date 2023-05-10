namespace Fohjin.DDD.Commands
{
    public record ClientIsMovingCommand : Command
    {
        public string Street { get; init; }
        public string StreetNumber { get; init; }
        public string PostalCode { get; init; }
        public string City { get; init; }

        public ClientIsMovingCommand(Guid id, string street, string streetNumber, string postalCode, string city) : base(id)
        {
            Street = street;
            StreetNumber = streetNumber;
            PostalCode = postalCode;
            City = city;
        }
    }
}