using Fohjin.DDD.Contracts;
using Fohjin.DDD.Events.Client;
using Fohjin.DDD.Reporting.Dto;

namespace Fohjin.DDD.EventHandlers
{
    public class ClientHasMovedEventHandler : IEventHandler<ClientHasMovedEvent>
    {
        private readonly IReportingRepository _reportingRepository;

        public ClientHasMovedEventHandler(IReportingRepository reportingRepository)
        {
            _reportingRepository = reportingRepository;
        }

        public void Execute(ClientHasMovedEvent theEvent)
        {
            _reportingRepository.Update<ClientDetails>(new { theEvent.Street, theEvent.StreetNumber, theEvent.PostalCode, theEvent.City }, new { Id = theEvent.AggregateId });
        }
    }
}