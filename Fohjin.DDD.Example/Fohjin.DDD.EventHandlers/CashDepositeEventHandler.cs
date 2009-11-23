using Fohjin.DDD.Events.Account;
using Fohjin.DDD.Reporting;
using Fohjin.DDD.Reporting.Dto;

namespace Fohjin.DDD.EventHandlers
{
    public class CashDepositeEventHandler : IEventHandler<CashDepositedEvent>
    {
        private readonly IReportingRepository _reportingRepository;

        public CashDepositeEventHandler(IReportingRepository reportingRepository)
        {
            _reportingRepository = reportingRepository;
        }

        public void Execute(CashDepositedEvent theEvent)
        {
            _reportingRepository.Update<AccountDetailsReport>(new {theEvent.Balance }, new { Id = theEvent.AggregateId });
            _reportingRepository.Save(new LedgerReport(theEvent.Id, theEvent.AggregateId, "Deposite", theEvent.Amount));
        }
    }
}