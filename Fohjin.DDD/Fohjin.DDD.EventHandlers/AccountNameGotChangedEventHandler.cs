using Fohjin.DDD.Events.ActiveAccount;
using Fohjin.DDD.Reporting.Dto;
using Fohjin.DDD.Reporting.Infrastructure;

namespace Fohjin.DDD.EventHandlers
{
    public class AccountNameGotChangedEventHandler : IEventHandler<AccountNameGotChangedEvent>
    {
        private readonly IReportingRepository _reportingRepository;

        public AccountNameGotChangedEventHandler(IReportingRepository reportingRepository)
        {
            _reportingRepository = reportingRepository;
        }

        public void Execute(AccountNameGotChangedEvent theEvent)
        {
            _reportingRepository.Update<Account>(new { Name = theEvent.AccountName }, new { Id = theEvent.EntityId });
            _reportingRepository.Update<AccountDetails>(new { theEvent.AccountName }, new { Id = theEvent.EntityId });
        }
    }
}