using Fohjin.DDD.Events.ActiveAccount;
using Fohjin.DDD.Reporting.Dto;
using Fohjin.DDD.Reporting.Infrastructure;

namespace Fohjin.DDD.EventHandlers
{
    public class DepositeEventHandler : IEventHandler<DepositeEvent>
    {
        private readonly IReportingRepository _reportingRepository;

        public DepositeEventHandler(IReportingRepository reportingRepository)
        {
            _reportingRepository = reportingRepository;
        }

        public void Execute(DepositeEvent theEvent)
        {
            _reportingRepository.Update<AccountDetails>(new {theEvent.Balance }, new { Id = theEvent.AggregateId });
            _reportingRepository.Save(new Ledger(theEvent.Id, theEvent.AggregateId, "Deposite", theEvent.Amount));
        }
    }
}