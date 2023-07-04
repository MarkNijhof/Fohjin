using System.Text.Json.Serialization;

namespace Fohjin.DDD.Commands
{
    public record ChangeClientNameCommand : CommandBase
    {
        public string? ClientName { get; init; }


        [JsonConstructor]
        public ChangeClientNameCommand() : base() { }
        public ChangeClientNameCommand(Guid id, string? clientName) : base(id)
        {
            ClientName = clientName;
        }
    }
}