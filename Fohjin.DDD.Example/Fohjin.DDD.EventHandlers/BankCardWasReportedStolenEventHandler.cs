using Fohjin.DDD.Events.Client;

namespace Fohjin.DDD.EventHandlers;

public class BankCardWasReportedStolenEventHandler : EventHandlerBase<BankCardWasReportedStolenEvent>
{
    public override Task ExecuteAsync(BankCardWasReportedStolenEvent theEvent)
    {
        throw new NotImplementedException();
    }
}