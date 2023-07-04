using System.Text.Json.Serialization;

namespace Fohjin.DDD.Commands;

public record ChangeAccountNameCommand : CommandBase
{
    public string? AccountName { get; init; }

    [JsonConstructor]
    public ChangeAccountNameCommand() : base() { }

    public ChangeAccountNameCommand(Guid id, string? accountName) : base(id)
    {
        AccountName = accountName;
    }
}