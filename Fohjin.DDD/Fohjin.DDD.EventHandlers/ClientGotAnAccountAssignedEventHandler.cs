using Fohjin.DDD.Events.Client;

namespace Fohjin.DDD.EventHandlers
{
    public class ClientGotAnAccountAssignedEventHandler : IEventHandler<ClientGotAnAccountAssignedEvent>
    {
        public void Execute(ClientGotAnAccountAssignedEvent theEvent)
        {
        }
    }
}