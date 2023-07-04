using System.Text.Json.Serialization;

namespace Fohjin.DDD.Commands;

public record OpenNewAccountForClientCommand : CommandBase
{
    public string? AccountName { get; init; }

    [JsonConstructor]
    public OpenNewAccountForClientCommand() : base() { }
    public OpenNewAccountForClientCommand(Guid id, string? accountName) : base(id)
    {
        AccountName = accountName;
    }
}