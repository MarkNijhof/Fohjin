using Fohjin.DDD.Events.Client;
using Fohjin.DDD.Reporting;

namespace Fohjin.DDD.EventHandlers;

public class BankCardWasReportedStolenEventHandler : EventHandlerBase<BankCardWasReportedStolenEvent>
{
    private readonly IReportingRepository _reportingRepository;

    public BankCardWasReportedStolenEventHandler(IReportingRepository reportingRepository)
    {
        _reportingRepository = reportingRepository;
    }

    public override Task ExecuteAsync(BankCardWasReportedStolenEvent theEvent)
    {
        throw new NotImplementedException();
    }
}