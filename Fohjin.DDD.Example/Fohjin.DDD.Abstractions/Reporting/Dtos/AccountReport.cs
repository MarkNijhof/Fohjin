using System.Text.Json.Serialization;

namespace Fohjin.DDD.Reporting.Dtos
{
    public class AccountReport
    {
        public Guid Id { get; set; }
        public Guid ClientDetailsReportId { get; set; }
        public string AccountName { get; set; }
        public string AccountNumber { get; set; }

        [JsonConstructor]
        public AccountReport() { }

        [SqliteConstructor]
        public AccountReport(Guid id, Guid clientDetailsReportId, string accountName, string accountNumber)
        {
            Id = id;
            ClientDetailsReportId = clientDetailsReportId;
            AccountName = accountName;
            AccountNumber = accountNumber;
        }

        public override string ToString()
        {
            return string.Format("{0} - ({1})", AccountNumber, AccountName);
        }
    }
}