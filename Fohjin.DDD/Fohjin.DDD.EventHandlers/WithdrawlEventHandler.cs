using Fohjin.DDD.Events.ActiveAccount;
using Fohjin.DDD.Reporting.Dto;
using Fohjin.DDD.Reporting.Infrastructure;

namespace Fohjin.DDD.EventHandlers
{
    public class WithdrawlEventHandler : IEventHandler<WithdrawlEvent>
    {
        private readonly IReportingRepository _reportingRepository;

        public WithdrawlEventHandler(IReportingRepository reportingRepository)
        {
            _reportingRepository = reportingRepository;
        }

        public void Execute(WithdrawlEvent theEvent)
        {
            _reportingRepository.Update<AccountDetails>(new {theEvent.Balance }, new { Id = theEvent.AggregateId });
            _reportingRepository.Save(new Ledger(theEvent.Id, theEvent.AggregateId, "Withdrawl", theEvent.Amount));
        }
    }
}