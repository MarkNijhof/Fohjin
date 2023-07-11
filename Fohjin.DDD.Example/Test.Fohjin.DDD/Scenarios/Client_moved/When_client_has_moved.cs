using Fohjin.DDD.EventHandlers;
using Fohjin.DDD.Events.Client;
using Fohjin.DDD.Reporting;
using Fohjin.DDD.Reporting.Dtos;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace Test.Fohjin.DDD.Scenarios.Client_moved;

public class When_client_has_moved : EventTestFixture<ClientMovedEvent, ClientMovedEventHandler>
{
    private static Guid _clientId;
    private object? UpdateClientDetailsObject;
    private object? WhereClientDetailsObject;

    protected override void SetupDependencies()
    {
        OnDependency<IReportingRepository>()
            .Setup(x => x.Update<ClientDetailsReport>(It.IsAny<object>(), It.IsAny<object>()))
            .Callback<object, object>((u, w) => { UpdateClientDetailsObject = u; WhereClientDetailsObject = w; });
    }

    protected override ClientMovedEvent When()
    {
        var clientHasMovedEvent = new ClientMovedEvent("Street", "123", "5000", "Bergen") { AggregateId = Guid.NewGuid() };
        _clientId = clientHasMovedEvent.AggregateId;
        return clientHasMovedEvent;
    }

    [TestMethod]
    public void Then_the_reporting_repository_will_be_used_to_update_the_client_details_report()
    {
        OnDependency<IReportingRepository>().Verify(x => x.Update<ClientDetailsReport>(It.IsAny<object>(), It.IsAny<object>()));
    }

    [TestMethod]
    public void Then_the_client_details_report_will_be_updated_with_the_expected_details()
    {
        UpdateClientDetailsObject.WillBeSimuliar(new { Street = "Street", StreetNumber = "123", PostalCode = "5000", City = "Bergen" }.ToString() ?? "");
        WhereClientDetailsObject.WillBeSimuliar(new { Id = _clientId });
    }
}