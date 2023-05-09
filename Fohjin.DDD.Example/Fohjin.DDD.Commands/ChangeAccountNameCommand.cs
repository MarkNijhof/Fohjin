namespace Fohjin.DDD.Commands
{
    public record ChangeAccountNameCommand : Command
    {
        public string AccountName { get; init; }

        public ChangeAccountNameCommand(Guid id, string accountName) : base(id)
        {
            AccountName = accountName;
        }
    }
}