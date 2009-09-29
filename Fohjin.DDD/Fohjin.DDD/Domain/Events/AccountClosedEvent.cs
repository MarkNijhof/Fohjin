using System;

namespace Fohjin.DDD.Domain.Events
{
    [Serializable]
    public class AccountClosedEvent : IDomainEvent
    {
        public AccountClosedEvent()
        {
            Id = Guid.NewGuid();
            TimeStamp = DateTime.Now;
        }
        public Guid Id { get; private set; }
        public DateTime TimeStamp { get; private set; }
    }
}