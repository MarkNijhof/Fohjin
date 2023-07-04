using System.Text.Json.Serialization;

namespace Fohjin.DDD.Domain.Client;

public record Address
{
    public string? Street { get; init; }
    public string? StreetNumber { get; init; }
    public string? PostalCode { get; init; }
    public string? City { get; init; }

    [JsonConstructor]
    public Address() { }
    public Address(string? street, string? streetNumber, string? postalCode, string? city)
    {
        Street = street;
        StreetNumber = streetNumber;
        PostalCode = postalCode;
        City = city;
    }
}