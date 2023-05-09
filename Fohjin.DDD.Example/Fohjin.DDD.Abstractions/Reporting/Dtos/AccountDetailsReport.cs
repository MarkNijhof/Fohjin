namespace Fohjin.DDD.Reporting.Dtos
{
    public class AccountDetailsReport
    {
        public Guid Id { get; init; }
        public Guid ClientReportId { get; init; }
        public IEnumerable<LedgerReport> Ledgers { get; init; }
        public string AccountName { get; init; }
        public decimal Balance { get; set; }
        public string AccountNumber { get; init; }

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