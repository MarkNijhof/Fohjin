using Fohjin.DDD.Events.Client;
using Fohjin.DDD.Reporting.Dto;
using Fohjin.DDD.Reporting.Infrastructure;

namespace Fohjin.DDD.EventHandlers
{
    public class NewClientCreatedEventHandler : IEventHandler<NewClientCreatedEvent>
    {
        private readonly IReportingRepository _reportingRepository;

        public NewClientCreatedEventHandler(IReportingRepository reportingRepository)
        {
            _reportingRepository = reportingRepository;
        }

        public void Execute(NewClientCreatedEvent theEvent)
        {
            var client = new Client(theEvent.ClientId, theEvent.ClientName);
            var clientDetails = new ClientDetails(theEvent.ClientId, theEvent.ClientName, theEvent.Street, theEvent.StreetNumber, theEvent.PostalCode, theEvent.City, theEvent.PhoneNumber);
            _reportingRepository.Save(client);
            _reportingRepository.Save(clientDetails);
        }
    }
}