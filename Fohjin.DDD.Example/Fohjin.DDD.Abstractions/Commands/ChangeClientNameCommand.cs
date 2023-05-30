namespace Fohjin.DDD.Commands
{
    public class ChangeClientNameCommand : Command
    {
        public string ClientName { get; set; }

        public ChangeClientNameCommand(Guid id, string clientName) : base(id)
        {
            ClientName = clientName;
        }
    }
}