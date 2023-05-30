namespace Fohjin.DDD.Commands
{
    public class ChangeAccountNameCommand : Command
    {
        public string AccountName { get; set; }

        public ChangeAccountNameCommand(Guid id, string accountName) : base(id)
        {
            AccountName = accountName;
        }
    }
}