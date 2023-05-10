namespace Fohjin.DDD.Commands
{
    public record OpenNewAccountForClientCommand : Command
    {
        public string AccountName { get; init; }

        public OpenNewAccountForClientCommand(Guid id, string accountName) : base(id)
        {
            AccountName = accountName;
        }
    }
}