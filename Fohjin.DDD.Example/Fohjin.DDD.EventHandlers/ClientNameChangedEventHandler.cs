using Fohjin.DDD.Events.Client;
using Fohjin.DDD.Reporting;
using Fohjin.DDD.Reporting.Dtos;

namespace Fohjin.DDD.EventHandlers
{
    public class ClientNameChangedEventHandler : EventHandlerBase<ClientNameChangedEvent>
    {
        private readonly IReportingRepository _reportingRepository;

        public ClientNameChangedEventHandler(IReportingRepository reportingRepository)
        {
            _reportingRepository = reportingRepository;
        }

        public override Task ExecuteAsync(ClientNameChangedEvent theEvent)
        {
            _reportingRepository.Update<ClientReport>(new { Name = theEvent.ClientName }, new { Id = theEvent.AggregateId });
            _reportingRepository.Update<ClientDetailsReport>(new { theEvent.ClientName }, new { Id = theEvent.AggregateId });

            return Task.CompletedTask;
        }
    }
}