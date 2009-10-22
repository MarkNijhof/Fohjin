using System;

namespace Fohjin.DDD.Reporting.Dto
{
    public class ClosedAccount : Account
    {
        public ClosedAccount(Guid id, Guid clientDetailsId, string name, string accountNumber) : base(id, clientDetailsId, name, accountNumber)
        {
        }
    }
}