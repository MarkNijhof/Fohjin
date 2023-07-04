using System.Text.Json.Serialization;

namespace Fohjin.DDD.Reporting.Dtos
{
    public record ClosedAccountReport : AccountReport
    {
        [JsonConstructor]
        public ClosedAccountReport()
        {
        }

        [SqliteConstructor]
        public ClosedAccountReport(
            Guid id,
            Guid clientDetailsId, 
            string? name, 
            string? accountNumber
            ) :
            base(id, clientDetailsId, name, accountNumber)
        {
        }
    }
}