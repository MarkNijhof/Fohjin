using System.Text.Json.Serialization;

namespace Fohjin.DDD.Events.Client;

public record ClientNameChangedEvent : DomainEvent
{
    public string? ClientName { get; set; }


    [JsonConstructor]
    public ClientNameChangedEvent() { }
    public ClientNameChangedEvent(string? clientName)
    {
        ClientName = clientName;
    }
}