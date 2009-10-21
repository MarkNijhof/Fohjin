using Fohjin.DDD.Events.ActiveAccount;
using Fohjin.DDD.Reporting.Dto;
using Fohjin.DDD.Reporting.Infrastructure;

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
            _reportingRepository.Update<Account>(new { Active = false }, new { Id = theEvent.AggregateId });
            _reportingRepository.Update<AccountDetails>(new { Active = false }, new { Id = theEvent.AggregateId });
        }
    }
}