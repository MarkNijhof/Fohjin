using System;

namespace Fohjin.DDD.Events.Client
{
    [Serializable]
    public class AccountWasAssignedToClientEvent : DomainEvent
    {
        public Guid ClientId { get; private set; }
        public Guid AccountId { get; private set; }

        public AccountWasAssignedToClientEvent(Guid clientId, Guid accountId)
        {
            ClientId = clientId;
            AccountId = accountId;
        }
    }
}