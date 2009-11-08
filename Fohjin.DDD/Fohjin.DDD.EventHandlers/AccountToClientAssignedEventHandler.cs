using Fohjin.DDD.Events.Client;

namespace Fohjin.DDD.EventHandlers
{
    public class AccountToClientAssignedEventHandler : IEventHandler<AccountToClientAssignedEvent>
    {
        public void Execute(AccountToClientAssignedEvent theEvent)
        {
        }
    }
}