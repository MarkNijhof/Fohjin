using System.Text.Json.Serialization;

namespace Fohjin.DDD.Commands
{
    public class OpenNewAccountForClientCommand : CommandBase
    {
        public string AccountName { get; set; }

        [JsonConstructor]
        public OpenNewAccountForClientCommand() : base() { }
        public OpenNewAccountForClientCommand(Guid id, string accountName) : base(id)
        {
            AccountName = accountName;
        }
    }
}