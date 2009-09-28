using System;

namespace Fohjin.DDD.Domain.Events
{
    public class AccountCreatedEvent : IDomainEvent
    {
        public AccountCreatedEvent(Guid guid)
        {
            TimeStamp = DateTime.Now.Ticks;
            Guid = guid;
        }
        public long TimeStamp { get; private set; }
        public Guid Guid { get; set; }
    }
}