using Fohjin.DDD.Events.Client;
using Fohjin.DDD.Reporting;

namespace Fohjin.DDD.EventHandlers
{
    public class BankCardWasCanceledByClientEventHandler : IEventHandler<BankCardWasCanceledByCLientEvent>
    {
        private readonly IReportingRepository _reportingRepository;

        public BankCardWasCanceledByClientEventHandler(IReportingRepository reportingRepository)
        {
            _reportingRepository = reportingRepository;
        }

        public void Execute(BankCardWasCanceledByCLientEvent theEvent)
        {
            throw new NotImplementedException();
        }
    }
}