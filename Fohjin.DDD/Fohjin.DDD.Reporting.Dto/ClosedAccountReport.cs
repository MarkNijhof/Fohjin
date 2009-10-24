using System;

namespace Fohjin.DDD.Reporting.Dto
{
    public class ClosedAccountReport : AccountReport
    {
        public ClosedAccountReport(Guid id, Guid clientDetailsId, string name, string accountNumber) : base(id, clientDetailsId, name, accountNumber)
        {
        }
    }
}