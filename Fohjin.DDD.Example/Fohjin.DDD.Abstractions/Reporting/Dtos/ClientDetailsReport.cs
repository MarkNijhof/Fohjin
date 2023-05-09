namespace Fohjin.DDD.Reporting.Dtos
{
    public class ClientDetailsReport
    {
        public Guid Id { get; init; }
        public IEnumerable<AccountReport> Accounts { get; init; }
        public IEnumerable<ClosedAccountReport> ClosedAccounts { get; init; }
        public string ClientName { get; init; }
        public string Street { get; init; }
        public string StreetNumber { get; init; }
        public string PostalCode { get; init; }
        public string City { get; init; }
        public string PhoneNumber { get; set; }

        public ClientDetailsReport(Guid id, string clientName, string street, string streetNumber, string postalCode, string city, string phoneNumber)
        {
            Id = id;
            Accounts = new List<AccountReport>();
            ClosedAccounts = new List<ClosedAccountReport>();
            ClientName = clientName;
            Street = street;
            StreetNumber = streetNumber;
            PostalCode = postalCode;
            City = city;
            PhoneNumber = phoneNumber;
        }
    }
}