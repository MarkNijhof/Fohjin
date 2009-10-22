using System;

namespace Fohjin.DDD.Reporting.Dto
{
    public class ClosedAccountDetails : AccountDetails
    {
        public ClosedAccountDetails(Guid id, Guid clientId, string accountName, decimal balance, string accountNumber) : base(id, clientId, accountName, balance, accountNumber)
        {
        }
    }
}