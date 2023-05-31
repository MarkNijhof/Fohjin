using System.Text.Json.Serialization;

namespace Fohjin.DDD.Commands
{
    public class ChangeAccountNameCommand : CommandBase
    {
        public string AccountName { get; set; }

        [JsonConstructor]
        public ChangeAccountNameCommand() : base() { }

        public ChangeAccountNameCommand(Guid id, string accountName) : base(id)
        {
            AccountName = accountName;
        }
    }
}