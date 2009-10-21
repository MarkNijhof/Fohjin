using Fohjin.DDD.Events.ActiveAccount;
using Fohjin.DDD.Reporting.Dto;
using Fohjin.DDD.Reporting.Infrastructure;

namespace Fohjin.DDD.EventHandlers
{
    public class AccountCreatedEventHandler : IEventHandler<AccountCreatedEvent>
    {
        private readonly IReportingRepository _reportingRepository;

        public AccountCreatedEventHandler(IReportingRepository reportingRepository)
        {
            _reportingRepository = reportingRepository;
        }

        public void Execute(AccountCreatedEvent theEvent)
        {
            var account = new Account(theEvent.AccountId, theEvent.ClientId, theEvent.AccountName, theEvent.AccountNumber, true);
            var accountDetails = new AccountDetails(theEvent.AccountId, theEvent.ClientId, theEvent.AccountName, 0.0M, theEvent.AccountNumber, true);
            _reportingRepository.Save(account);
            _reportingRepository.Save(accountDetails);
        }
    }
}