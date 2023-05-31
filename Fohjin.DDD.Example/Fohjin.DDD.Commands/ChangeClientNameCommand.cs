namespace Fohjin.DDD.Commands
{
    public record ChangeClientNameCommand : Command
    {
        public string ClientName { get; init; }

        public ChangeClientNameCommand(Guid id, string clientName) : base(id)
        {
            ClientName = clientName;
        }
    }
}