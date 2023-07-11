namespace Fohjin.DDD.Commands
{
    public record CancelBankCardCommand : Command
    {
        public Guid BankCardId { get; init; }

        public CancelBankCardCommand(Guid id, Guid bankCardId) : base(id)
        {
            BankCardId = bankCardId;
        }
    }
}