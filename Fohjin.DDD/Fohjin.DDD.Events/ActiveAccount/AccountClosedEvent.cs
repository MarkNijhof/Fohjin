using System;

namespace Fohjin.DDD.Events.ActiveAccount
{
    [Serializable]
    public class AccountClosedEvent : IDomainEvent
    {
        public AccountClosedEvent()
        {
            Id = Guid.NewGuid();
        }
        public Guid Id { get; private set; }
        int IDomainEvent.Version { get; set; }
    }
}