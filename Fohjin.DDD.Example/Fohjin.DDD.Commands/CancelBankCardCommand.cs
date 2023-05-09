namespace Fohjin.DDD.Commands
{
    [Serializable]
    public class CancelBankCardCommand : Command
    {
        public Guid BankCardId { get; init; }

        public CancelBankCardCommand(Guid id, Guid bankCardId) : base(id)
        {
            BankCardId = bankCardId;
        }
    }
}