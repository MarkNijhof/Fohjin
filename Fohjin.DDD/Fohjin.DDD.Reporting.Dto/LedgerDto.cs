using System;

namespace Fohjin.DDD.Reporting.Dto
{
    public class LedgerDto
    {
        public Guid Id { get; private set; }
        public string Action { get; private set; }
        public decimal Amount { get; private set; }

        public LedgerDto(Guid id, string action, decimal amount)
        {
            Id = id;
            Action = action;
            Amount = amount;
        }

        public override string ToString()
        {
            return string.Format("{0} - {1:C}", Action, Amount);
        }
    }
}