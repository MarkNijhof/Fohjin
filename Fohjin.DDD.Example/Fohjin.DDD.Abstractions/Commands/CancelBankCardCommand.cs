using System.Text.Json.Serialization;

namespace Fohjin.DDD.Commands
{
    public record CancelBankCardCommand : CommandBase
    {
        public Guid BankCardId { get; init; }

        [JsonConstructor]
        public CancelBankCardCommand(): base() { }

        public CancelBankCardCommand(Guid id, Guid bankCardId) : base(id)
        {
            BankCardId = bankCardId;
        }
    }
}