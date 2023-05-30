namespace Fohjin.DDD.Events.Client
{
    public class AccountToClientAssignedEvent : DomainEvent
    {
        public Guid AccountId { get; set; }

        public AccountToClientAssignedEvent(Guid accountId)
        {
            AccountId = accountId;
        }
    }
}