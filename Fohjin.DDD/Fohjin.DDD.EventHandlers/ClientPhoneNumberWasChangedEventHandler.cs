using Fohjin.DDD.Events.Client;
using Fohjin.DDD.Reporting.Dto;
using Fohjin.DDD.Reporting.Infrastructure;

namespace Fohjin.DDD.EventHandlers
{
    public class ClientPhoneNumberWasChangedEventHandler : IEventHandler<ClientPhoneNumberWasChangedEvent>
    {
        private readonly IReportingRepository _reportingRepository;

        public ClientPhoneNumberWasChangedEventHandler(IReportingRepository reportingRepository)
        {
            _reportingRepository = reportingRepository;
        }

        public void Execute(ClientPhoneNumberWasChangedEvent theEvent)
        {
            _reportingRepository.Update<ClientDetails>(new { theEvent.PhoneNumber }, new { Id = theEvent.AggregateId });
        }
    }
}