using System.Text.Json.Serialization;

namespace Fohjin.DDD.Reporting.Dtos
{
    public class LedgerReport
    {
        public Guid Id { get; set; }
        public Guid AccountDetailsReportId { get; set; }
        public string Action { get; set; } = null!;
        public decimal Amount { get; set; }

        [JsonConstructor]
        public LedgerReport() { }

        [SqliteConstructor]
        public LedgerReport(Guid id, Guid accountDetailsReportId, string action, decimal amount)
        {
            Id = id;
            AccountDetailsReportId = accountDetailsReportId;
            Action = action;
            Amount = amount;
        }

        public override string ToString() => "{Action} - {Amount:C}";
    }
}