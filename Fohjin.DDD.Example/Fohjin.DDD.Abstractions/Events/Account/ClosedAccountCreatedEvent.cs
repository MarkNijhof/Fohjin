namespace Fohjin.DDD.Events.Account
{
    public record ClosedAccountCreatedEvent : DomainEvent
    {
        public Guid AccountId { get; init; }
        public Guid OriginalAccountId { get; init; }
        public Guid ClientId { get; init; }
        public IList<KeyValuePair<string, string>> Ledgers { get; init; }
        public string AccountName { get; init; }
        public string AccountNumber { get; init; }

        public ClosedAccountCreatedEvent(Guid accountId, Guid originalAccountId, Guid clientId, IList<KeyValuePair<string, string>> ledgers, string accountName, string accountNumber)
        {
            AccountId = accountId;
            OriginalAccountId = originalAccountId;
            ClientId = clientId;
            Ledgers = ledgers;
            AccountName = accountName;
            AccountNumber = accountNumber;
        }
    }
}