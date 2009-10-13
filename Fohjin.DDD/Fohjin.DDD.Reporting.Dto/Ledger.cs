using System;

namespace Fohjin.DDD.Reporting.Dto
{
    public class Ledger
    {
        public Guid Id { get; private set; }
        public Guid AccountDetailsId { get; private set; }
        public string Action { get; private set; }
        public decimal Amount { get; private set; }

        public Ledger(Guid id, Guid accountDetailsId, string action, decimal amount)
        {
            Id = id;
            AccountDetailsId = accountDetailsId;
            Action = action;
            Amount = amount;
        }

        public override string ToString()
        {
            return string.Format("{0} - {1:C}", Action, Amount);
        }
    }
}