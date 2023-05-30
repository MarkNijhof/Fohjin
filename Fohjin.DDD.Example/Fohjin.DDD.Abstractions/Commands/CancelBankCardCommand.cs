using System.Text.Json.Serialization;

namespace Fohjin.DDD.Commands
{
    public class CancelBankCardCommand : CommandBase
    {
        public Guid BankCardId { get; set; }

        [JsonConstructor]
        public CancelBankCardCommand(): base() { }

        public CancelBankCardCommand(Guid id, Guid bankCardId) : base(id)
        {
            BankCardId = bankCardId;
        }
    }
}