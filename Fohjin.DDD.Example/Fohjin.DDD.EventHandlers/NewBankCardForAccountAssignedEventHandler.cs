using Fohjin.DDD.Events.Client;
using Fohjin.DDD.Reporting;

namespace Fohjin.DDD.EventHandlers;

public class NewBankCardForAccountAssignedEventHandler : EventHandlerBase<NewBankCardForAccountAsignedEvent>
{
    private readonly IReportingRepository _reportingRepository;

    public NewBankCardForAccountAssignedEventHandler(IReportingRepository reportingRepository)
    {
        _reportingRepository = reportingRepository;
    }

    public override Task ExecuteAsync(NewBankCardForAccountAsignedEvent theEvent)
    {
        throw new NotImplementedException();
    }
}