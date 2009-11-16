using System;
using Fohjin.DDD.Events.Client;
using Fohjin.DDD.Reporting;

namespace Fohjin.DDD.EventHandlers
{
    public class BankCardWasReportedStolenEventHandler : IEventHandler<BankCardWasReportedStolenEvent>
    {
        private readonly IReportingRepository _reportingRepository;

        public BankCardWasReportedStolenEventHandler(IReportingRepository reportingRepository)
        {
            _reportingRepository = reportingRepository;
        }

        public void Execute(BankCardWasReportedStolenEvent theEvent)
        {
            throw new NotImplementedException();
        }
    }
}