namespace Fohjin.DDD.Reporting.Dtos
{
    public class LedgerReport
    {
        public Guid Id { get; init; }
        public Guid AccountDetailsReportId { get; init; }
        public string Action { get; init; }
        public decimal Amount { get; init; }

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