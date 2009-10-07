using System;

namespace Fohjin.DDD.Events.ActiveAccount
{
    [Serializable]
    public class AccountCreatedEvent : IDomainEvent
    {
        public AccountCreatedEvent(Guid accountId, string accountName)
        {
            Id = Guid.NewGuid();
            AccountId = accountId;
            AccountName = accountName;
        }
        public Guid Id { get; private set; }
        int IDomainEvent.Version { get; set; }

        public Guid AccountId { get; set; }
        public string AccountName { get; set; }
    }
}