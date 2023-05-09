namespace Fohjin.DDD.Events.Client
{
    public record AccountToClientAssignedEvent : DomainEvent
    {
        public Guid AccountId { get; init; }

        public AccountToClientAssignedEvent(Guid accountId)
        {
            AccountId = accountId;
        }
    }
}