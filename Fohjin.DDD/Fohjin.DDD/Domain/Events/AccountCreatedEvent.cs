using System;

namespace Fohjin.DDD.Domain.Events
{
    public class AccountCreatedEvent : IDomainEvent
    {
        public AccountCreatedEvent(Guid guid)
        {
            Guid = guid;
        }

        public Guid Guid { get; set; }
    }
}