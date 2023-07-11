using System.Text.Json.Serialization;

namespace Fohjin.DDD.Events.Client;

public record ClientCreatedEvent : DomainEvent
{
    public Guid ClientId { get; set; }
    public string? ClientName { get; set; }
    public string? Street { get; set; }
    public string? StreetNumber { get; set; }
    public string? PostalCode { get; set; }
    public string? City { get; set; }
    public string? PhoneNumber { get; set; }

    [JsonConstructor]
    public ClientCreatedEvent() { }

    public ClientCreatedEvent(
        Guid clientId,
        string? clientName,
        string? street,
        string? streetNumber,
        string? postalCode,
        string? city,
        string? phoneNumber
        )
    {
        ClientId = clientId;
        ClientName = clientName;
        Street = street;
        StreetNumber = streetNumber;
        PostalCode = postalCode;
        City = city;
        PhoneNumber = phoneNumber;
    }
}