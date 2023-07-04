using System.Text.Json.Serialization;

namespace Fohjin.DDD.Events.Account;

public record AccountOpenedEvent : DomainEvent
{
    public Guid AccountId { get; set; }
    public Guid ClientId { get; set; }
    public string? AccountName { get; set; }
    public string? AccountNumber { get; set; } 

    [JsonConstructor]
    public AccountOpenedEvent() { }
    public AccountOpenedEvent(Guid accountId, Guid clientId, string? accountName, string? accountNumber)
    {
        AccountId = accountId;
        ClientId = clientId;
        AccountName = accountName;
        AccountNumber = accountNumber;
    }
}