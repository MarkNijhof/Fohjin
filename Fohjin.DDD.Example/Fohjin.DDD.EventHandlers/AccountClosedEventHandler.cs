using Fohjin.DDD.Events.Account;
using Fohjin.DDD.Reporting;
using Fohjin.DDD.Reporting.Dto;

namespace Fohjin.DDD.EventHandlers
{
    public class AccountClosedEventHandler : IEventHandler<AccountClosedEvent>
    {
        private readonly IReportingRepository _reportingRepository;

        public AccountClosedEventHandler(IReportingRepository reportingRepository)
        {
            _reportingRepository = reportingRepository;
        }

        public void Execute(AccountClosedEvent theEvent)
        {
            _reportingRepository.Delete<AccountReport>(new { Id = theEvent.AggregateId });
            _reportingRepository.Delete<AccountDetailsReport>(new { Id = theEvent.AggregateId });
        }
    }
}