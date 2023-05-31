using System.Text.Json.Serialization;

namespace Fohjin.DDD.Reporting.Dtos
{
    public class ClientDetailsReport
    {
        public Guid Id { get; set; }
        public List<AccountReport> Accounts { get; set; } = new();
        public List<ClosedAccountReport> ClosedAccounts { get; set; } = new();
        public string ClientName { get; set; } = null!;
        public string Street { get; set; } = null!;
        public string StreetNumber { get; set; } = null!;
        public string PostalCode { get; set; } = null!;
        public string City { get; set; } = null!;
        public string PhoneNumber { get; set; } = null!;

        [JsonConstructor]
        public ClientDetailsReport()
        {
        }

        [SqliteConstructor]
        public ClientDetailsReport(Guid id, string clientName, string street, string streetNumber, string postalCode, string city, string phoneNumber)
        {
            Id = id;
            ClientName = clientName;
            Street = street;
            StreetNumber = streetNumber;
            PostalCode = postalCode;
            City = city;
            PhoneNumber = phoneNumber;
        }
    }
}