using System.Text.Json.Serialization;

namespace Fohjin.DDD.Commands
{
    public class CloseAccountCommand : CommandBase
    {
        [JsonConstructor]
        public CloseAccountCommand() : base() { }
        public CloseAccountCommand(Guid id) : base(id) { }
    }
}