using Fohjin.DDD.Events.Account;
using Fohjin.DDD.Reporting;
using Fohjin.DDD.Reporting.Dtos;

namespace Fohjin.DDD.EventHandlers
{
    public class CashDepositEventHandler : IEventHandler<CashDepositdEvent>
    {
        private readonly IReportingRepository _reportingRepository;

        public CashDepositEventHandler(IReportingRepository reportingRepository)
        {
            _reportingRepository = reportingRepository;
        }

        public void Execute(CashDepositdEvent theEvent)
        {
            _reportingRepository.Update<AccountDetailsReport>(new {theEvent.Balance }, new { Id = theEvent.AggregateId });
            _reportingRepository.Save(new LedgerReport(theEvent.Id, theEvent.AggregateId, "Deposit", theEvent.Amount));
        }
    }
}