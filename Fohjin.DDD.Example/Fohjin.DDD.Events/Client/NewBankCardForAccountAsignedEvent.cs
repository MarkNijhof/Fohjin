using System;

namespace Fohjin.DDD.Events.Client
{
    [Serializable]
    public class NewBankCardForAccountAsignedEvent : DomainEvent
    {
        public Guid BankCardId { get; private set; }
        public Guid AccountId { get; private set; }

        public NewBankCardForAccountAsignedEvent(Guid bankCardId, Guid accountId)
        {
            BankCardId = bankCardId;
            AccountId = accountId;
        }
    }
}