using System.Text.Json.Serialization;

namespace Fohjin.DDD.Commands;

public record CreateClientCommand : CommandBase
{
    public string? ClientName { get; init; }
    public string? Street { get; init; }
    public string? StreetNumber { get; init; }
    public string? PostalCode { get; init; }
    public string? City { get; init; }
    public string? PhoneNumber { get; init; }

    [JsonConstructor]
    public CreateClientCommand() : base() { }
    public CreateClientCommand(Guid id,
        string? clientName,
        string? street,
        string? streetNumber,
        string? postalCode,
        string? city,
        string? phoneNumber
        ) : base(id)
    {
        ClientName = clientName;
        Street = street;
        StreetNumber = streetNumber;
        PostalCode = postalCode;
        City = city;
        PhoneNumber = phoneNumber;
    }
}