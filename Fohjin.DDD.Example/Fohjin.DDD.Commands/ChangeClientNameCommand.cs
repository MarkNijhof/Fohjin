namespace Fohjin.DDD.Commands
{
    [Serializable]
    public class ChangeClientNameCommand : Command
    {
        public string ClientName { get; init; }

        public ChangeClientNameCommand(Guid id, string clientName) : base(id)
        {
            ClientName = clientName;
        }
    }
}