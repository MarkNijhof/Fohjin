using System.Text.Json.Serialization;

namespace Fohjin.DDD.Reporting.Dtos
{
    public record ClosedAccountDetailsReport : AccountDetailsReport
    {
        [JsonConstructor]
        public ClosedAccountDetailsReport(): base() { }

        [SqliteConstructor]
        public ClosedAccountDetailsReport(
            Guid id, 
            Guid clientId,
            string? accountName, 
            decimal balance, 
            string? accountNumber
            ) :
            base(id, clientId, accountName, balance, accountNumber)
        {
        }
    }
}