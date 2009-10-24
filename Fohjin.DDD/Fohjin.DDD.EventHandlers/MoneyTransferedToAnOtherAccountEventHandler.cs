using Fohjin.DDD.Contracts;
using Fohjin.DDD.Events.ActiveAccount;
using Fohjin.DDD.Reporting.Dto;

namespace Fohjin.DDD.EventHandlers
{
    public class MoneyTransferedToAnOtherAccountEventHandler : IEventHandler<MoneyTransferSendToAnOtherAccountEvent>
    {
        private readonly IReportingRepository _reportingRepository;

        public MoneyTransferedToAnOtherAccountEventHandler(IReportingRepository reportingRepository)
        {
            _reportingRepository = reportingRepository;
        }

        public void Execute(MoneyTransferSendToAnOtherAccountEvent theEvent)
        {
            _reportingRepository.Update<AccountDetails>(new {theEvent.Balance }, new { Id = theEvent.AggregateId });
            _reportingRepository.Save(new Ledger(theEvent.Id, theEvent.AggregateId, string.Format("Transfer to {0}", theEvent.TargetAccount), theEvent.Amount));
        }
    }
}