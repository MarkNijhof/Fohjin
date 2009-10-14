using System;

namespace Fohjin.DDD.Commands
{
    public class ClientChangedTheirNameCommand : Command
    {
        public string ClientName { get; private set; }

        public ClientChangedTheirNameCommand(Guid id, string clientName) : base(id)
        {
            ClientName = clientName;
        }
    }
}