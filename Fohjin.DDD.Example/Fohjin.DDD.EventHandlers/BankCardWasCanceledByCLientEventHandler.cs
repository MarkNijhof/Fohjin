using Fohjin.DDD.Events.Client;
using Fohjin.DDD.Reporting;

namespace Fohjin.DDD.EventHandlers
{
    public class BankCardWasCanceledByClientEventHandler : EventHandlerBase<BankCardWasCanceledByCLientEvent>
    {
        private readonly IReportingRepository _reportingRepository;

        public BankCardWasCanceledByClientEventHandler(IReportingRepository reportingRepository)
        {
            _reportingRepository = reportingRepository;
        }

        public override Task ExecuteAsync(BankCardWasCanceledByCLientEvent theEvent)
        {
            throw new NotImplementedException();
            return Task.CompletedTask;
        }
    }
}