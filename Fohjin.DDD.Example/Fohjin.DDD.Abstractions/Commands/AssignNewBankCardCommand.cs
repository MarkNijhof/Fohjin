namespace Fohjin.DDD.Commands
{
    public record AssignNewBankCardCommand : Command
    {
        public Guid AccountId { get; set; }

        public AssignNewBankCardCommand(Guid id, Guid accountId) : base(id)
        {
            AccountId = accountId;
        }
    }
}