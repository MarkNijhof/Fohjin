using System;
using Fohjin.DDD.Events.Client;
using Fohjin.DDD.Reporting;

namespace Fohjin.DDD.EventHandlers
{
    public class BankCardWasCanceledByCLientEventHandler : IEventHandler<BankCardWasCanceledByCLientEvent>
    {
        private readonly IReportingRepository _reportingRepository;

        public BankCardWasCanceledByCLientEventHandler(IReportingRepository reportingRepository)
        {
            _reportingRepository = reportingRepository;
        }

        public void Execute(BankCardWasCanceledByCLientEvent theEvent)
        {
            throw new NotImplementedException();
        }
    }
}