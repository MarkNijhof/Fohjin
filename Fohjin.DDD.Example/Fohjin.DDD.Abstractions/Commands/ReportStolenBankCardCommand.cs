namespace Fohjin.DDD.Commands
{
    public class ReportStolenBankCardCommand : Command
    {
        public Guid BankCardId { get; set; }

        public ReportStolenBankCardCommand(Guid id, Guid bankCardId) : base(id)
        {
            BankCardId = bankCardId;
        }
    }
}