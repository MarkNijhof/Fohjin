using Fohjin.DDD.Contracts;
using Fohjin.DDD.Events.ActiveAccount;
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
            _reportingRepository.Delete<Account>(new { Id = theEvent.AggregateId });
            _reportingRepository.Delete<AccountDetails>(new { Id = theEvent.AggregateId });
        }
    }
}