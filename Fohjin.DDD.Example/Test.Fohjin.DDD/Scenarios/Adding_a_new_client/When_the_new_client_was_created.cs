using Fohjin.DDD.EventHandlers;
using Fohjin.DDD.Events.Client;
using Fohjin.DDD.Reporting;
using Fohjin.DDD.Reporting.Dtos;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace Test.Fohjin.DDD.Scenarios.Adding_a_new_client;

public class When_the_new_client_was_created : EventTestFixture<ClientCreatedEvent, ClientCreatedEventHandler>
{
    private static Guid _clientId;
    private ClientReport SaveClientObject = null!;
    private ClientDetailsReport SaveClientDetailsObject = null!;

    protected override void SetupDependencies()
    {
        OnDependency<IReportingRepository>()
            .Setup(x => x.Save(It.IsAny<ClientReport>()))
            .Callback<ClientReport>(a => SaveClientObject = a);

        OnDependency<IReportingRepository>()
            .Setup(x => x.Save(It.IsAny<ClientDetailsReport>()))
            .Callback<ClientDetailsReport>(a => SaveClientDetailsObject = a);
    }

    protected override ClientCreatedEvent When()
    {
        _clientId = Guid.NewGuid();
        return new ClientCreatedEvent(_clientId, "New Client Name", "Street", "123", "5000", "Bergen", "1234567890");
    }

    [TestMethod]
    public void Then_the_reporting_repository_will_be_used_to_save_the_client_report()
    {
        OnDependency<IReportingRepository>().Verify(x => x.Save(It.IsAny<ClientReport>()));
    }

    [TestMethod]
    public void Then_the_client_report_will_be_updated_with_the_expected_details()
    {
        SaveClientObject.Id.WillBe(_clientId);
        SaveClientObject.Name.WillBe("New Client Name");
    }

    [TestMethod]
    public void Then_the_reporting_repository_will_be_used_to_save_the_client_details_report()
    {
        OnDependency<IReportingRepository>().Verify(x => x.Save(It.IsAny<ClientDetailsReport>()));
    }

    [TestMethod]
    public void Then_the_client_details_report_will_be_updated_with_the_expected_details()
    {
        SaveClientDetailsObject.Id.WillBe(_clientId);
        SaveClientDetailsObject.ClientName.WillBe("New Client Name");
        SaveClientDetailsObject.Street.WillBe("Street");
        SaveClientDetailsObject.StreetNumber.WillBe("123");
        SaveClientDetailsObject.PostalCode.WillBe("5000");
        SaveClientDetailsObject.City.WillBe("Bergen");
        SaveClientDetailsObject.PhoneNumber.WillBe("1234567890");
    }
}