namespace Fohjin.DDD.Events.Account
{
    public class ClosedAccountCreatedEvent : DomainEvent
    {
        public Guid AccountId { get; set; }
        public Guid OriginalAccountId { get; set; }
        public Guid ClientId { get; set; }
        public IList<KeyValuePair<string, string>> Ledgers { get; set; }
        public string AccountName { get; set; }
        public string AccountNumber { get; set; }

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