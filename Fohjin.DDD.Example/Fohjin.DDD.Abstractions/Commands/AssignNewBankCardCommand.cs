namespace Fohjin.DDD.Commands
{
    public class AssignNewBankCardCommand : Command
    {
        public Guid AccountId { get; set; }

        public AssignNewBankCardCommand(Guid id, Guid accountId) : base(id)
        {
            AccountId = accountId;
        }
    }
}