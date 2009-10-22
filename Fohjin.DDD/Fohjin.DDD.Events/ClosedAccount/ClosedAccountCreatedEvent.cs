using System;
using System.Collections.Generic;

namespace Fohjin.DDD.Events.ClosedAccount
{
    [Serializable]
    public class ClosedAccountCreatedEvent : DomainEvent
    {
        public Guid AccountId { get; private set; }
        public Guid ClientId { get; private set; }
        public IList<KeyValuePair<string, string>> Ledgers { get; private set; }
        public string AccountName { get; private set; }
        public string AccountNumber { get; private set; }

        public ClosedAccountCreatedEvent(Guid accoutId, Guid clientId, IList<KeyValuePair<string, string>> ledgers, string accountName, string accountNumber)
        {
            AccountId = accoutId;
            ClientId = clientId;
            Ledgers = ledgers;
            AccountName = accountName;
            AccountNumber = accountNumber;
        }
    }
}