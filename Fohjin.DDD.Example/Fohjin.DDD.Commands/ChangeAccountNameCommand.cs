namespace Fohjin.DDD.Commands
{
    [Serializable]
    public class ChangeAccountNameCommand : Command
    {
        public string AccountName { get; init; }

        public ChangeAccountNameCommand(Guid id, string accountName) : base(id)
        {
            AccountName = accountName;
        }
    }
}