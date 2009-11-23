using System;
using Fohjin.DDD.Events.Client;
using Fohjin.DDD.Reporting;

namespace Fohjin.DDD.EventHandlers
{
    public class NewBankCardForAccountAssignedEventHandler : IEventHandler<NewBankCardForAccountAsignedEvent>
    {
        private readonly IReportingRepository _reportingRepository;

        public NewBankCardForAccountAssignedEventHandler(IReportingRepository reportingRepository)
        {
            _reportingRepository = reportingRepository;
        }

        public void Execute(NewBankCardForAccountAsignedEvent theEvent)
        {
            throw new NotImplementedException();
        }
    }
}