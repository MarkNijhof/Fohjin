using Fohjin.DDD.Events.Client;
using Fohjin.DDD.Reporting;
using Fohjin.DDD.Reporting.Dto;

namespace Fohjin.DDD.EventHandlers
{
    public class ClientPhoneNumberChangedEventHandler : IEventHandler<ClientPhoneNumberChangedEvent>
    {
        private readonly IReportingRepository _reportingRepository;

        public ClientPhoneNumberChangedEventHandler(IReportingRepository reportingRepository)
        {
            _reportingRepository = reportingRepository;
        }

        public void Execute(ClientPhoneNumberChangedEvent theEvent)
        {
            _reportingRepository.Update<ClientDetailsReport>(new { theEvent.PhoneNumber }, new { Id = theEvent.AggregateId });
        }
    }
}