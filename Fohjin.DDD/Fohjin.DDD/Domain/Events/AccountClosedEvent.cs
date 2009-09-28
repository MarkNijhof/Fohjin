using System;

namespace Fohjin.DDD.Domain.Events
{
    public class AccountClosedEvent : IDomainEvent
    {
        public AccountClosedEvent()
        {
            TimeStamp = DateTime.Now.Ticks;
        }
        public long TimeStamp { get; private set; }
    }
}