using System;

namespace Fohjin.DDD.Reporting.Dto
{
    public class AccountReport
    {
        public Guid Id { get; private set; }
        public Guid ClientDetailsReportId { get; private set; }
        public string AccountName { get; private set; }
        public string AccountNumber { get; private set; }

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