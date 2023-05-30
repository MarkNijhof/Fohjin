using System.Text.Json.Serialization;

namespace Fohjin.DDD.Events.Client
{
    public class AccountToClientAssignedEvent : DomainEvent
    {
        public Guid AccountId { get; set; }

        [JsonConstructor]
        public AccountToClientAssignedEvent() { }
        public AccountToClientAssignedEvent(Guid accountId)
        {
            AccountId = accountId;
        }
    }
}