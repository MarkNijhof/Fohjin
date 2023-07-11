using System.Text.Json.Serialization;

namespace Fohjin.DDD.Commands
{
    public record CloseAccountCommand : CommandBase
    {
        [JsonConstructor]
        public CloseAccountCommand() : base() { }
        public CloseAccountCommand(Guid id) : base(id) { }
    }
}