using System;

namespace Fohjin.DDD.Events.ActiveAccount
{
    [Serializable]
    public class AccountNameChangedEvent : DomainEvent 
    {
        public string AccountName { get; private set; }

        public AccountNameChangedEvent(string accountName)
        {
            AccountName = accountName;
        }
    }
}