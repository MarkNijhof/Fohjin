namespace Fohjin.DDD.Commands
{
    [Serializable]
    public class OpenNewAccountForClientCommand : Command
    {
        public string AccountName { get; init; }

        public OpenNewAccountForClientCommand(Guid id, string accountName) : base(id)
        {
            AccountName = accountName;
        }
    }
}