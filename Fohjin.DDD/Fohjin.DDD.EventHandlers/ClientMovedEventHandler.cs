using Fohjin.DDD.Events.Client;
using Fohjin.DDD.Reporting;
using Fohjin.DDD.Reporting.Dto;

namespace Fohjin.DDD.EventHandlers
{
    public class ClientMovedEventHandler : IEventHandler<ClientMovedEvent>
    {
        private readonly IReportingRepository _reportingRepository;

        public ClientMovedEventHandler(IReportingRepository reportingRepository)
        {
            _reportingRepository = reportingRepository;
        }

        public void Execute(ClientMovedEvent theEvent)
        {
            _reportingRepository.Update<ClientDetailsReport>(new { theEvent.Street, theEvent.StreetNumber, theEvent.PostalCode, theEvent.City }, new { Id = theEvent.AggregateId });
        }
    }
}