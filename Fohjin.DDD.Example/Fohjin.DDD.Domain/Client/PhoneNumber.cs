using System.Text.Json.Serialization;

namespace Fohjin.DDD.Domain.Client;

public record PhoneNumber
{
    public string? Number { get; init; }

    [JsonConstructor]
    public PhoneNumber() { }

    public PhoneNumber(string? number)
    {
        Number = number;
    }
}