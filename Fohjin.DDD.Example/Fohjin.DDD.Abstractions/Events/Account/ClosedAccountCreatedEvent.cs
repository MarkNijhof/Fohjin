using System.Text.Json.Serialization;

namespace Fohjin.DDD.Events.Account
{
    public class ClosedAccountCreatedEvent : DomainEvent
    {
        public Guid AccountId { get; set; }
        public Guid OriginalAccountId { get; set; }
        public Guid ClientId { get; set; }
        public List<KeyValuePair<string, string>> Ledgers { get; set; } = new();
        public string AccountName { get; set; } = null!;
        public string AccountNumber { get; set; } = null!;


        [JsonConstructor]
        public ClosedAccountCreatedEvent() { }
        public ClosedAccountCreatedEvent(
            Guid accountId, 
            Guid originalAccountId,
            Guid clientId,
            List<KeyValuePair<string, string>> ledgers,
            string accountName, 
            string accountNumber)
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