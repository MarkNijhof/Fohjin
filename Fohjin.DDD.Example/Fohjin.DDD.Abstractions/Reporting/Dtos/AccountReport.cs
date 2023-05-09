namespace Fohjin.DDD.Reporting.Dtos
{
    public class AccountReport
    {
        public Guid Id { get; init; }
        public Guid ClientDetailsReportId { get; init; }
        public string AccountName { get; init; }
        public string AccountNumber { get; init; }

        public AccountReport(Guid id, Guid clientDetailsId, string accountName, string accountNumber)
        {
            Id = id;
            ClientDetailsReportId = clientDetailsId;
            AccountName = accountName;
            AccountNumber = accountNumber;
        }

        public override string ToString()
        {
            return string.Format("{0} - ({1})", AccountNumber, AccountName);
        }
    }
}