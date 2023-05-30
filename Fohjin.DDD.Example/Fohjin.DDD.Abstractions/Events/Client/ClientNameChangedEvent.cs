namespace Fohjin.DDD.Events.Client
{
    public class ClientNameChangedEvent : DomainEvent
    {
        public string ClientName { get; set; }

        public ClientNameChangedEvent(string clientName)
        {
            ClientName = clientName;
        }
    }
}