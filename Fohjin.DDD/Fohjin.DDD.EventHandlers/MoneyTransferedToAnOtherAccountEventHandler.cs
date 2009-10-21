using Fohjin.DDD.Events.ActiveAccount;
using Fohjin.DDD.Reporting.Dto;
using Fohjin.DDD.Reporting.Infrastructure;

namespace Fohjin.DDD.EventHandlers
{
    public class MoneyTransferedToAnOtherAccountEventHandler : IEventHandler<MoneyTransferedToAnOtherAccountEvent>
    {
        private readonly IReportingRepository _reportingRepository;

        public MoneyTransferedToAnOtherAccountEventHandler(IReportingRepository reportingRepository)
        {
            _reportingRepository = reportingRepository;
        }

        public void Execute(MoneyTransferedToAnOtherAccountEvent theEvent)
        {
            _reportingRepository.Update<AccountDetails>(new {theEvent.Balance }, new { Id = theEvent.AggregateId });
            _reportingRepository.Save(new Ledger(theEvent.Id, theEvent.AggregateId, string.Format("Transfer to {0}", theEvent.OtherAccount), theEvent.Amount));
        }
    }
}