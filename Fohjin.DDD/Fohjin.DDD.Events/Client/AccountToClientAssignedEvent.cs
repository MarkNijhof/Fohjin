using System;

namespace Fohjin.DDD.Events.Client
{
    [Serializable]
    public class AccountToClientAssignedEvent : DomainEvent
    {
        public Guid AccountId { get; private set; }

        public AccountToClientAssignedEvent(Guid accountId)
        {
            AccountId = accountId;
        }
    }
}