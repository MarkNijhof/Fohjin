namespace Fohjin.DDD.Commands
{
    public class OpenNewAccountForClientCommand : Command
    {
        public string AccountName { get; set; }

        public OpenNewAccountForClientCommand(Guid id, string accountName) : base(id)
        {
            AccountName = accountName;
        }
    }
}