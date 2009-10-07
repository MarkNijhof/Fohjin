using System;

namespace Fohjin.DDD.Events.ActiveAccount
{
    [Serializable]
    public class DepositeEvent : IDomainEvent
    {
        public DepositeEvent(decimal balance, decimal amount)
        {
            Id = Guid.NewGuid();
            Balance = balance;
            Amount = amount;
        }
        public Guid Id { get; private set; }
        int IDomainEvent.Version { get; set; }

        public decimal Balance { get; private set; }
        public decimal Amount { get; private set; }
    }
}