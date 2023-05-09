namespace Fohjin.DDD.Reporting.Dtos
{
    public class ClosedAccountDetailsReport : AccountDetailsReport
    {
        public ClosedAccountDetailsReport(Guid id, Guid clientId, string accountName, decimal balance, string accountNumber) : base(id, clientId, accountName, balance, accountNumber)
        {
        }
    }
}