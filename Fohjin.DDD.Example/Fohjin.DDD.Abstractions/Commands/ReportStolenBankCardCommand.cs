using System.Text.Json.Serialization;

namespace Fohjin.DDD.Commands
{
    public class ReportStolenBankCardCommand : CommandBase
    {
        public Guid BankCardId { get; set; }


        [JsonConstructor]
        public ReportStolenBankCardCommand() : base() { }

        public ReportStolenBankCardCommand(Guid id, Guid bankCardId) : base(id)
        {
            BankCardId = bankCardId;
        }
    }
}