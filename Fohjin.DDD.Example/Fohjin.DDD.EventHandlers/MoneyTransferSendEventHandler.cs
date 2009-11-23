using Fohjin.DDD.Events.Account;
using Fohjin.DDD.Reporting;
using Fohjin.DDD.Reporting.Dto;

namespace Fohjin.DDD.EventHandlers
{
    public class MoneyTransferSendEventHandler : IEventHandler<MoneyTransferSendEvent>
    {
        private readonly IReportingRepository _reportingRepository;

        public MoneyTransferSendEventHandler(IReportingRepository reportingRepository)
        {
            _reportingRepository = reportingRepository;
        }

        public void Execute(MoneyTransferSendEvent theEvent)
        {
            _reportingRepository.Update<AccountDetailsReport>(new {theEvent.Balance }, new { Id = theEvent.AggregateId });
            _reportingRepository.Save(new LedgerReport(theEvent.Id, theEvent.AggregateId, string.Format("Transfer to {0}", theEvent.TargetAccount), theEvent.Amount));
        }
    }
}