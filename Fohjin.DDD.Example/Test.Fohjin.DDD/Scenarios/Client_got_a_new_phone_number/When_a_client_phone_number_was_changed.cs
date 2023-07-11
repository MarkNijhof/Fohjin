using Fohjin.DDD.EventHandlers;
using Fohjin.DDD.Events.Client;
using Fohjin.DDD.Reporting;
using Fohjin.DDD.Reporting.Dtos;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace Test.Fohjin.DDD.Scenarios.Client_got_a_new_phone_number;

public class When_a_client_phone_number_was_changed : EventTestFixture<ClientPhoneNumberChangedEvent, ClientPhoneNumberChangedEventHandler>
{
    private static Guid _clientId;
    private object? UpdateObject;
    private object? WhereObject;

    protected override void SetupDependencies()
    {
        OnDependency<IReportingRepository>()
            .Setup(x => x.Update<ClientDetailsReport>(It.IsAny<object>(), It.IsAny<object>()))
            .Callback<object, object>((u, w) => { UpdateObject = u; WhereObject = w; });
    }

    protected override ClientPhoneNumberChangedEvent When()
    {
        var clientPhoneNumberWasChangedEvent = new ClientPhoneNumberChangedEvent("1234567890") { AggregateId = Guid.NewGuid() };
        _clientId = clientPhoneNumberWasChangedEvent.AggregateId;
        return clientPhoneNumberWasChangedEvent;
    }

    [TestMethod]
    public void Then_the_reporting_repository_will_be_used_to_update_the_client_details_report()
    {
        OnDependency<IReportingRepository>().Verify(x => x.Update<ClientDetailsReport>(It.IsAny<object>(), It.IsAny<object>()));
    }

    [TestMethod]
    public void Then_the_client_details_report_will_be_updated_with_the_expected_details()
    {
        UpdateObject.WillBeSimuliar(new { PhoneNumber = "1234567890" }.ToString() ?? "");
        WhereObject.WillBeSimuliar(new { Id = _clientId });
    }
}