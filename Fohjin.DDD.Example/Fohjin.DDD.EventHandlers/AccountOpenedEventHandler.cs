using Fohjin.DDD.Events.Account;
using Fohjin.DDD.Reporting;
using Fohjin.DDD.Reporting.Dtos;

namespace Fohjin.DDD.EventHandlers
{
    public class AccountOpenedEventHandler : EventHandlerBase<AccountOpenedEvent>
    {
        private readonly IReportingRepository _reportingRepository;

        public AccountOpenedEventHandler(IReportingRepository reportingRepository)
        {
            _reportingRepository = reportingRepository;
        }

        public override Task ExecuteAsync(AccountOpenedEvent theEvent)
        {
            var account = new AccountReport(theEvent.AccountId, theEvent.ClientId, theEvent.AccountName, theEvent.AccountNumber);
            var accountDetails = new AccountDetailsReport(theEvent.AccountId, theEvent.ClientId, theEvent.AccountName, 0.0M, theEvent.AccountNumber);
            _reportingRepository.Save(account);
            _reportingRepository.Save(accountDetails);
            return Task.CompletedTask;
        }
    }
}