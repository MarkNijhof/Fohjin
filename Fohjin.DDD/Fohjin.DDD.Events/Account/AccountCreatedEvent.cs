using System;

namespace Fohjin.DDD.Events.Account
{
    [Serializable]
    public class AccountCreatedEvent : DomainEvent
    {
        public Guid AccountId { get; private set; }
        public Guid ClientId { get; private set; }
        public string AccountName { get; private set; }
        public string AccountNumber { get; private set; }

        public AccountCreatedEvent(Guid accountId, Guid clientId, string accountName, string accountNumber)
        {
            AccountId = accountId;
            ClientId = clientId;
            AccountName = accountName;
            AccountNumber = accountNumber;
        }
    }
}