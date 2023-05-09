namespace Fohjin.DDD.Events.Account
{
    public record AccountNameChangedEvent : DomainEvent
    {
        public string AccountName { get; init; }

        public AccountNameChangedEvent(string accountName)
        {
            AccountName = accountName;
        }
    }
}