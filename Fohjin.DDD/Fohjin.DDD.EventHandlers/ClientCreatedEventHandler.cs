using Fohjin.DDD.Contracts;
using Fohjin.DDD.Events.Client;
using Fohjin.DDD.EventStore.Bus;
using Fohjin.DDD.Reporting.Dto;

namespace Fohjin.DDD.EventHandlers
{
    public class ClientCreatedEventHandler : IEventHandler<ClientCreatedEvent>
    {
        private readonly IReportingRepository _reportingRepository;

        public ClientCreatedEventHandler(IReportingRepository reportingRepository)
        {
            _reportingRepository = reportingRepository;
        }

        public void Execute(ClientCreatedEvent theEvent)
        {
            var client = new ClientReport(theEvent.ClientId, theEvent.ClientName);
            var clientDetails = new ClientDetailsReport(theEvent.ClientId, theEvent.ClientName, theEvent.Street, theEvent.StreetNumber, theEvent.PostalCode, theEvent.City, theEvent.PhoneNumber);
            _reportingRepository.Save(client);
            _reportingRepository.Save(clientDetails);
        }
    }
}