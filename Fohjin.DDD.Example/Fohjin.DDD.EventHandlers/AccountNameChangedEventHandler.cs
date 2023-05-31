using Fohjin.DDD.Events.Account;
using Fohjin.DDD.Reporting;
using Fohjin.DDD.Reporting.Dtos;

namespace Fohjin.DDD.EventHandlers
{
    public class AccountNameChangedEventHandler : EventHandlerBase<AccountNameChangedEvent>
    {
        private readonly IReportingRepository _reportingRepository;

        public AccountNameChangedEventHandler(IReportingRepository reportingRepository)
        {
            _reportingRepository = reportingRepository;
        }

        public override Task ExecuteAsync(AccountNameChangedEvent theEvent)
        {
            _reportingRepository.Update<AccountReport>(new { theEvent.AccountName }, new { Id = theEvent.AggregateId });
            _reportingRepository.Update<AccountDetailsReport>(new { theEvent.AccountName }, new { Id = theEvent.AggregateId });
            return Task.CompletedTask;
        }
    }
}