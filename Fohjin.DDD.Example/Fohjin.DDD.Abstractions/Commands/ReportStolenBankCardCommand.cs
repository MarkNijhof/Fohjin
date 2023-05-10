namespace Fohjin.DDD.Commands
{
    public record ReportStolenBankCardCommand : Command
    {
        public Guid BankCardId { get; init; }

        public ReportStolenBankCardCommand(Guid id, Guid bankCardId) : base(id)
        {
            BankCardId = bankCardId;
        }
    }
}