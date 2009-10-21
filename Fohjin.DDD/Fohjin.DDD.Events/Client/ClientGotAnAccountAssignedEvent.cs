using System;

namespace Fohjin.DDD.Events.Client
{
    [Serializable]
    public class ClientGotAnAccountAssignedEvent : DomainEvent
    {
        public Guid AccountId { get; private set; }

        public ClientGotAnAccountAssignedEvent(Guid accountId)
        {
            AccountId = accountId;
        }
    }
}