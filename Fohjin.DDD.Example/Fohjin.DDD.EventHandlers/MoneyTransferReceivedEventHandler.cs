using Fohjin.DDD.Events.Account;
using Fohjin.DDD.Reporting;
using Fohjin.DDD.Reporting.Dto;

namespace Fohjin.DDD.EventHandlers
{
    public class MoneyTransferReceivedEventHandler : IEventHandler<MoneyTransferReceivedEvent>
    {
        private readonly IReportingRepository _reportingRepository;

        public MoneyTransferReceivedEventHandler(IReportingRepository reportingRepository)
        {
            _reportingRepository = reportingRepository;
        }

        public void Execute(MoneyTransferReceivedEvent theEvent)
        {
            _reportingRepository.Update<AccountDetailsReport>(new {theEvent.Balance }, new { Id = theEvent.AggregateId });
            _reportingRepository.Save(new LedgerReport(theEvent.Id, theEvent.AggregateId, string.Format("Transfer from {0}", theEvent.SourceAccount), theEvent.Amount));
        }
    }
}