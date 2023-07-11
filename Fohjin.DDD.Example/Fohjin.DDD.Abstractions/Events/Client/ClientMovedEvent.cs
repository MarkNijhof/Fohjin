using System.Text.Json.Serialization;

namespace Fohjin.DDD.Events.Client;

public record ClientMovedEvent : DomainEvent
{
    public string? Street { get; init; }
    public string? StreetNumber { get; init; }
    public string? PostalCode { get; init; }
    public string? City { get; init; }

    [JsonConstructor]
    public ClientMovedEvent() { }

    public ClientMovedEvent(string? street, string? streetNumber, string? postalCode, string? city)
    {
        Street = street;
        StreetNumber = streetNumber;
        PostalCode = postalCode;
        City = city;
    }
}