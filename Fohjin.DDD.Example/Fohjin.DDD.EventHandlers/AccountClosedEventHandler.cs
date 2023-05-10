using Fohjin.DDD.Events.Account;
using Fohjin.DDD.Reporting;
using Fohjin.DDD.Reporting.Dtos;

namespace Fohjin.DDD.EventHandlers
{
    public class AccountClosedEventHandler : EventHandlerBase<AccountClosedEvent>
    {
        private readonly IReportingRepository _reportingRepository;

        public AccountClosedEventHandler(IReportingRepository reportingRepository)
        {
            _reportingRepository = reportingRepository;
        }

        public override Task ExecuteAsync(AccountClosedEvent theEvent)
        {
            _reportingRepository.Delete<AccountReport>(new { Id = theEvent.AggregateId });
            _reportingRepository.Delete<AccountDetailsReport>(new { Id = theEvent.AggregateId });
            return Task.CompletedTask;
        }
    }
}