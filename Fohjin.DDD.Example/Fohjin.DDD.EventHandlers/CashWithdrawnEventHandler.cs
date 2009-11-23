using Fohjin.DDD.Events.Account;
using Fohjin.DDD.Reporting;
using Fohjin.DDD.Reporting.Dto;

namespace Fohjin.DDD.EventHandlers
{
    public class CashWithdrawnEventHandler : IEventHandler<CashWithdrawnEvent>
    {
        private readonly IReportingRepository _reportingRepository;

        public CashWithdrawnEventHandler(IReportingRepository reportingRepository)
        {
            _reportingRepository = reportingRepository;
        }

        public void Execute(CashWithdrawnEvent theEvent)
        {
            _reportingRepository.Update<AccountDetailsReport>(new {theEvent.Balance }, new { Id = theEvent.AggregateId });
            _reportingRepository.Save(new LedgerReport(theEvent.Id, theEvent.AggregateId, "Withdrawl", theEvent.Amount));
        }
    }
}