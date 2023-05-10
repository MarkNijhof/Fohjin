using Fohjin.DDD.Events.Account;
using Fohjin.DDD.Reporting;
using Fohjin.DDD.Reporting.Dtos;

namespace Fohjin.DDD.EventHandlers
{
    public class MoneyTransferReceivedEventHandler : EventHandlerBase<MoneyTransferReceivedEvent>
    {
        private readonly IReportingRepository _reportingRepository;

        public MoneyTransferReceivedEventHandler(IReportingRepository reportingRepository)
        {
            _reportingRepository = reportingRepository;
        }

        public override Task ExecuteAsync(MoneyTransferReceivedEvent theEvent)
        {
            _reportingRepository.Update<AccountDetailsReport>(new { theEvent.Balance }, new { Id = theEvent.AggregateId });
            _reportingRepository.Save(new LedgerReport(theEvent.Id, theEvent.AggregateId, string.Format("Transfer from {0}", theEvent.SourceAccount), theEvent.Amount));

            return Task.CompletedTask;
        }
    }
}