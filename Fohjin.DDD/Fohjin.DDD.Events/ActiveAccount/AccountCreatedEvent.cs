using System;

namespace Fohjin.DDD.Events.ActiveAccount
{
    [Serializable]
    public class AccountCreatedEvent : IDomainEvent
    {
        public Guid Id { get; private set; }
        int IDomainEvent.Version { get; set; }

        public Guid AccountId { get; private set; }
        public string AccountName { get; private set; }

        public AccountCreatedEvent(Guid accountId, string accountName)
        {
            Id = Guid.NewGuid();
            AccountId = accountId;
            AccountName = accountName;
        }
    }
}