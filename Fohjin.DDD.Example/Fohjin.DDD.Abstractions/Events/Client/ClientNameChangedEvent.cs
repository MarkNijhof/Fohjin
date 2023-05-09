namespace Fohjin.DDD.Events.Client
{
    public record ClientNameChangedEvent : DomainEvent
    {
        public string ClientName { get; init; }

        public ClientNameChangedEvent(string cLientName)
        {
            ClientName = cLientName;
        }
    }
}