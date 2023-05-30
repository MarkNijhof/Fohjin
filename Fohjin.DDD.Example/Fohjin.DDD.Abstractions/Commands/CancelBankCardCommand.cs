namespace Fohjin.DDD.Commands
{
    public class CancelBankCardCommand : Command
    {
        public Guid BankCardId { get; set; }

        public CancelBankCardCommand(Guid id, Guid bankCardId) : base(id)
        {
            BankCardId = bankCardId;
        }
    }
}