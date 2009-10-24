using System;

namespace Fohjin.DDD.Reporting.Dto
{
    public class LedgerReport
    {
        public Guid Id { get; private set; }
        public Guid AccountDetailsReportId { get; private set; }
        public string Action { get; private set; }
        public decimal Amount { get; private set; }

        public LedgerReport(Guid id, Guid accountDetailsId, string action, decimal amount)
        {
            Id = id;
            AccountDetailsReportId = accountDetailsId;
            Action = action;
            Amount = amount;
        }

        public override string ToString()
        {
            return string.Format("{0} - {1:C}", Action, Amount);
        }
    }
}