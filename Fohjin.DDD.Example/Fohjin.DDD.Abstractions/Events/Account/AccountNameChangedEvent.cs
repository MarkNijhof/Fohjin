using System.Text.Json.Serialization;

namespace Fohjin.DDD.Events.Account;

public record AccountNameChangedEvent : DomainEvent
{
    public string? AccountName { get; set; }

    [JsonConstructor]
    public AccountNameChangedEvent() { }

    public AccountNameChangedEvent(string? accountName)
    {
        AccountName = accountName;
    }
}