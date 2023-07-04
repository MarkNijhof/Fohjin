using System.Text.Json.Serialization;

namespace Fohjin.DDD.Commands
{
    public record AssignNewBankCardCommand : CommandBase
    {
        public Guid AccountId { get; init; }

        [JsonConstructor]
        public AssignNewBankCardCommand() : base() { }
        public AssignNewBankCardCommand(Guid id, Guid accountId) : base(id)
        {
            AccountId = accountId;
        }
    }
}