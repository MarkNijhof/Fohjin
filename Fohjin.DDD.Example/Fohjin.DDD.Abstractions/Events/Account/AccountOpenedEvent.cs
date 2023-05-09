namespace Fohjin.DDD.Events.Account
{
    public record AccountOpenedEvent : DomainEvent
    {
        public Guid AccountId { get; init; }
        public Guid ClientId { get; init; }
        public string AccountName { get; init; }
        public string AccountNumber { get; init; }

        public AccountOpenedEvent(Guid accountId, Guid clientId, string accountName, string accountNumber)
        {
            AccountId = accountId;
            ClientId = clientId;
            AccountName = accountName;
            AccountNumber = accountNumber;
        }
    }
}