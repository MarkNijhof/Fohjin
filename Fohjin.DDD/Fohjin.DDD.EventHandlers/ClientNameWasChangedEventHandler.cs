using Fohjin.DDD.Contracts;
using Fohjin.DDD.Events.Client;
using Fohjin.DDD.Reporting.Dto;

namespace Fohjin.DDD.EventHandlers
{
    public class ClientNameWasChangedEventHandler : IEventHandler<ClientNameWasChangedEvent>
    {
        private readonly IReportingRepository _reportingRepository;

        public ClientNameWasChangedEventHandler(IReportingRepository reportingRepository)
        {
            _reportingRepository = reportingRepository;
        }

        public void Execute(ClientNameWasChangedEvent theEvent)
        {
            _reportingRepository.Update<Client>(new { Name = theEvent.ClientName }, new { Id = theEvent.AggregateId });
            _reportingRepository.Update<ClientDetails>(new { theEvent.ClientName }, new { Id = theEvent.AggregateId });
        }
    }
}