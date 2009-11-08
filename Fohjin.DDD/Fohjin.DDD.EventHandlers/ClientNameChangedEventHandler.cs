using Fohjin.DDD.Events.Client;
using Fohjin.DDD.Reporting;
using Fohjin.DDD.Reporting.Dto;

namespace Fohjin.DDD.EventHandlers
{
    public class ClientNameChangedEventHandler : IEventHandler<ClientNameChangedEvent>
    {
        private readonly IReportingRepository _reportingRepository;

        public ClientNameChangedEventHandler(IReportingRepository reportingRepository)
        {
            _reportingRepository = reportingRepository;
        }

        public void Execute(ClientNameChangedEvent theEvent)
        {
            _reportingRepository.Update<ClientReport>(new { Name = theEvent.ClientName }, new { Id = theEvent.AggregateId });
            _reportingRepository.Update<ClientDetailsReport>(new { theEvent.ClientName }, new { Id = theEvent.AggregateId });
        }
    }
}