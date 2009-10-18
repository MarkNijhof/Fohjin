using System;

namespace Fohjin.DDD.Events.ActiveAccount
{
    [Serializable]
    public class AccountCreatedEvent : DomainEvent
    {
        public Guid AccountId { get; private set; }
        public string AccountName { get; private set; }

        public AccountCreatedEvent(Guid accountId, string accountName)
        {
            AccountId = accountId;
            AccountName = accountName;
        }
    }
}