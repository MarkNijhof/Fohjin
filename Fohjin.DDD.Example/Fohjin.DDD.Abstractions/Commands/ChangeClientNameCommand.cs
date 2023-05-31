using System.Text.Json.Serialization;

namespace Fohjin.DDD.Commands
{
    public class ChangeClientNameCommand : CommandBase
    {
        public string ClientName { get; set; }


        [JsonConstructor]
        public ChangeClientNameCommand() : base() { }
        public ChangeClientNameCommand(Guid id, string clientName) : base(id)
        {
            ClientName = clientName;
        }
    }
}