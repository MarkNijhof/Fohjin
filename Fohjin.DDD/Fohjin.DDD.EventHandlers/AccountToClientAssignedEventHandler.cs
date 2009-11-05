using Fohjin.DDD.Events.Client;
using Fohjin.DDD.EventStore.Bus;

namespace Fohjin.DDD.EventHandlers
{
    public class AccountToClientAssignedEventHandler : IEventHandler<AccountToClientAssignedEvent>
    {
        public void Execute(AccountToClientAssignedEvent theEvent)
        {
        }
    }
}