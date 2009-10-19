using System;

namespace Fohjin.DDD.Events.ActiveAccount
{
    [Serializable]
    public class AccountNameGotChangedEvent : DomainEvent 
    {
        public string AccountName { get; private set; }

        public AccountNameGotChangedEvent(string accountName)
        {
            AccountName = accountName;
        }
    }
}