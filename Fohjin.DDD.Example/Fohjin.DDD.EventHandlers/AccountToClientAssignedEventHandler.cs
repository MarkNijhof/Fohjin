using Fohjin.DDD.Events.Client;

namespace Fohjin.DDD.EventHandlers
{
    public class AccountToClientAssignedEventHandler : EventHandlerBase<AccountToClientAssignedEvent>
    {
        public override Task ExecuteAsync(AccountToClientAssignedEvent theEvent)
        {
            return Task.CompletedTask;
        }
    }
}