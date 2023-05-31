using System.Text.Json.Serialization;

namespace Fohjin.DDD.Events.Account
{
    public class AccountNameChangedEvent : DomainEvent
    {
        public string AccountName { get; set; } = null!;

        [JsonConstructor]
        public AccountNameChangedEvent() { }

        public AccountNameChangedEvent(string accountName)
        {
            AccountName = accountName;
        }
    }
}