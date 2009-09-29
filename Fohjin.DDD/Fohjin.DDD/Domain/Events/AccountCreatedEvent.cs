using System;

namespace Fohjin.DDD.Domain.Events
{
    [Serializable]
    public class AccountCreatedEvent : IDomainEvent
    {
        public AccountCreatedEvent(Guid guid)
        {
            Id = Guid.NewGuid();
            TimeStamp = DateTime.Now;
            Guid = guid;
        }
        public Guid Id { get; private set; }
        public DateTime TimeStamp { get; private set; }
        public Guid Guid { get; set; }
    }
}