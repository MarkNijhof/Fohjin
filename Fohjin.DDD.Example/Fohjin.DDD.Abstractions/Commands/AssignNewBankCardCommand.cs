using System.Text.Json.Serialization;

namespace Fohjin.DDD.Commands
{
    public class AssignNewBankCardCommand : CommandBase
    {
        public Guid AccountId { get; set; }

        [JsonConstructor]
        public AssignNewBankCardCommand() : base() { }
        public AssignNewBankCardCommand(Guid id, Guid accountId) : base(id)
        {
            AccountId = accountId;
        }
    }
}