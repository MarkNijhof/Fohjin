using Fohjin.DDD.Events.Client;

namespace Fohjin.DDD.EventHandlers;

public class BankCardWasCanceledByClientEventHandler : EventHandlerBase<BankCardWasCanceledByClientEvent>
{
    public override Task ExecuteAsync(BankCardWasCanceledByClientEvent theEvent)
    {
        throw new NotImplementedException();
    }
}