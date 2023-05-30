using System.Text.Json.Serialization;

namespace Fohjin.DDD.Events.Client
{
    public class ClientNameChangedEvent : DomainEvent
    {
        public string ClientName { get; set; } = null!;


        [JsonConstructor]
        public ClientNameChangedEvent() { }
        public ClientNameChangedEvent(string clientName)
        {
            ClientName = clientName;
        }
    }
}