namespace Fohjin.DDD.Events.Account
{
    public class AccountNameChangedEvent : DomainEvent
    {
        public string AccountName { get; set; }

        public AccountNameChangedEvent(string accountName)
        {
            AccountName = accountName;
        }
    }
}