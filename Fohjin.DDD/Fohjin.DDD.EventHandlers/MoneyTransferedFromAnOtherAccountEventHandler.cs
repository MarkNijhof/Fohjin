using Fohjin.DDD.Contracts;
using Fohjin.DDD.Events.ActiveAccount;
using Fohjin.DDD.Reporting.Dto;

namespace Fohjin.DDD.EventHandlers
{
    public class MoneyTransferedFromAnOtherAccountEventHandler : IEventHandler<MoneyTransferReceivedFromAnOtherAccountEvent>
    {
        private readonly IReportingRepository _reportingRepository;

        public MoneyTransferedFromAnOtherAccountEventHandler(IReportingRepository reportingRepository)
        {
            _reportingRepository = reportingRepository;
        }

        public void Execute(MoneyTransferReceivedFromAnOtherAccountEvent theEvent)
        {
            _reportingRepository.Update<AccountDetails>(new {theEvent.Balance }, new { Id = theEvent.AggregateId });
            _reportingRepository.Save(new Ledger(theEvent.Id, theEvent.AggregateId, string.Format("Transfer from {0}", theEvent.SourceAccount), theEvent.Amount));
        }
    }
}