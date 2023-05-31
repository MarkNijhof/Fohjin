using System.Text.Json.Serialization;

namespace Fohjin.DDD.Reporting.Dtos
{
    public class AccountDetailsReport
    {
        public Guid Id { get; set; }
        public Guid ClientReportId { get; set; }
        public List<LedgerReport> Ledgers { get; set; }
        public string AccountName { get; set; }
        public decimal Balance { get; set; }
        public string AccountNumber { get; set; }

        [JsonConstructor]
        public AccountDetailsReport()
        {
        }

        [SqliteConstructor]
        public AccountDetailsReport(Guid id, Guid clientReportId, string accountName, decimal balance, string accountNumber)
        {
            Id = id;
            ClientReportId = clientReportId;
            Ledgers = new List<LedgerReport>();
            AccountName = accountName;
            Balance = balance;
            AccountNumber = accountNumber;
        }
    }
}