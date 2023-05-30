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

        public AccountDetailsReport(Guid id, Guid clientId, string accountName, decimal balance, string accountNumber)
        {
            Id = id;
            ClientReportId = clientId;
            Ledgers = new List<LedgerReport>();
            AccountName = accountName;
            Balance = balance;
            AccountNumber = accountNumber;
        }
    }
}