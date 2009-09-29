using System;

namespace Fohjin.DDD.Domain.Events
{
    [Serializable]
    public class DepositeEvent : IDomainEvent
    {
        public DepositeEvent(decimal balance, decimal amount)
        {
            Id = Guid.NewGuid();
            TimeStamp = DateTime.Now;
            Balance = balance;
            Amount = amount;
        }
        public Guid Id { get; private set; }
        public DateTime TimeStamp { get; private set; }
        public decimal Balance { get; private set; }
        public decimal Amount { get; private set; }
    }
}