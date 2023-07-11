using System.Text.Json.Serialization;

namespace Fohjin.DDD.Reporting.Dtos
{
    public record ClientDetailsReport
    {
        public Guid Id { get; set; }
        public List<AccountReport> Accounts { get; set; } = new();
        public List<ClosedAccountReport> ClosedAccounts { get; set; } = new();
        public string? ClientName { get; set; }
        public string? Street { get; set; }
        public string? StreetNumber { get; set; }
        public string? PostalCode { get; set; }
        public string? City { get; set; }
        public string? PhoneNumber { get; set; }

        [JsonConstructor]
        public ClientDetailsReport()
        {
        }

        [SqliteConstructor]
        public ClientDetailsReport(
            Guid id,
            string? clientName,
            string? street,
            string? streetNumber,
            string? postalCode,
            string? city,
            string? phoneNumber
            )
        {
            Id = id;
            ClientName = clientName;
            Street = street;
            StreetNumber = streetNumber;
            PostalCode = postalCode;
            City = city;
            PhoneNumber = phoneNumber;
        }

        public static ClientDetailsReport New => new () { Id = Guid.NewGuid(), };
    }
}