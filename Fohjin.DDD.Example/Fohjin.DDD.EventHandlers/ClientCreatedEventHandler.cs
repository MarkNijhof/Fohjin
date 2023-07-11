using Fohjin.DDD.Events.Client;
using Fohjin.DDD.Reporting;
using Fohjin.DDD.Reporting.Dtos;

namespace Fohjin.DDD.EventHandlers;

public class ClientCreatedEventHandler : EventHandlerBase<ClientCreatedEvent>
{
    private readonly IReportingRepository _reportingRepository;

    public ClientCreatedEventHandler(IReportingRepository reportingRepository)
    {
        _reportingRepository = reportingRepository;
    }

    public override Task ExecuteAsync(ClientCreatedEvent theEvent)
    {
        var client = new ClientReport(theEvent.ClientId, theEvent.ClientName);
        var clientDetails = new ClientDetailsReport(theEvent.ClientId, theEvent.ClientName, theEvent.Street, theEvent.StreetNumber, theEvent.PostalCode, theEvent.City, theEvent.PhoneNumber);
        _reportingRepository.Save(client);
        _reportingRepository.Save(clientDetails);
        return Task.CompletedTask;
    }
}