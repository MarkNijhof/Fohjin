using System;

namespace Fohjin.DDD.Events.Account
{
    [Serializable]
    public class AccountOpenedEvent : DomainEvent
    {
        public Guid AccountId { get; private set; }
        public Guid ClientId { get; private set; }
        public string AccountName { get; private set; }
        public string AccountNumber { get; private set; }

        public AccountOpenedEvent(Guid accountId, Guid clientId, string accountName, string accountNumber)
        {
            AccountId = accountId;
            ClientId = clientId;
            AccountName = accountName;
            AccountNumber = accountNumber;
        }
    }
}