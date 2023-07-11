using System.Text.Json.Serialization;

namespace Fohjin.DDD.Commands
{
    public record ReportStolenBankCardCommand : CommandBase
    {
        public Guid BankCardId { get; init; }


        [JsonConstructor]
        public ReportStolenBankCardCommand() : base() { }

        public ReportStolenBankCardCommand(Guid id, Guid bankCardId) : base(id)
        {
            BankCardId = bankCardId;
        }
    }
}