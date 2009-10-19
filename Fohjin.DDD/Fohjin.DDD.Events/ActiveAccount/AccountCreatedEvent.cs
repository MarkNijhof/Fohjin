using System;

namespace Fohjin.DDD.Events.ActiveAccount
{
    [Serializable]
    public class AccountCreatedEvent : DomainEvent
    {
        public Guid AccountId { get; private set; }
        public string AccountName { get; private set; }
        public string AccountNumber { get; private set; }

        public AccountCreatedEvent(Guid accountId, string accountName, string accountNumber)
        {
            AccountId = accountId;
            AccountName = accountName;
            AccountNumber = accountNumber;
        }
    }
}