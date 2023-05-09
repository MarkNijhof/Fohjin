namespace Fohjin.DDD.Events.Client
{
    public record ClientCreatedEvent : DomainEvent
    {
        public Guid ClientId { get; init; }
        public string ClientName { get; init; }
        public string Street { get; init; }
        public string StreetNumber { get; init; }
        public string PostalCode { get; init; }
        public string City { get; init; }
        public string PhoneNumber { get; init; }

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