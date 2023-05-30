using System.Text.Json.Serialization;

namespace Fohjin.DDD.Reporting.Dtos
{
    public class ClientDetailsReport
    {
        public Guid Id { get; set; }
        public List<AccountReport> Accounts { get; set; }
        public List<ClosedAccountReport> ClosedAccounts { get; set; }
        public string ClientName { get; set; }
        public string Street { get; set; }
        public string StreetNumber { get; set; }
        public string PostalCode { get; set; }
        public string City { get; set; }
        public string PhoneNumber { get; set; }

        [JsonConstructor]
        public ClientDetailsReport()
        {
        }

        [SqliteConstructor]
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