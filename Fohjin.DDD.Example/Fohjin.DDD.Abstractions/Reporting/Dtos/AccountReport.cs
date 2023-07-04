using System.Text.Json.Serialization;

namespace Fohjin.DDD.Reporting.Dtos
{
    public record AccountReport
    {
        public Guid Id { get; init; }
        public Guid ClientDetailsReportId { get; init; }
        public string? AccountName { get; init; }
        public string? AccountNumber { get; init; }

        [JsonConstructor]
        public AccountReport() { }

        [SqliteConstructor]
        public AccountReport(
            Guid id,
            Guid clientDetailsReportId,
            string? accountName, 
            string? accountNumber
            )
        {
            Id = id;
            ClientDetailsReportId = clientDetailsReportId;
            AccountName = accountName;
            AccountNumber = accountNumber;
        }

        public override string ToString() => $"{AccountNumber} - ({AccountName})";
    }
}