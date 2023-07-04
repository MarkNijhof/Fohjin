using Fohjin.DDD.BankApplication.Presenters;
using Fohjin.DDD.BankApplication.Views;
using Fohjin.DDD.Reporting;
using Fohjin.DDD.Reporting.Dtos;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace Test.Fohjin.DDD.Scenarios.Client_moved;

public class When_in_the_GUI_a_client_is_moving_ : PresenterTestFixture<ClientDetailsPresenter>
{
    private readonly Guid _clientId = Guid.NewGuid();
    private ClientDetailsReport _clientDetailsReport = null!;
    private List<ClientDetailsReport>? _clientDetailsReports;

    protected override void SetupDependencies()
    {
        _clientDetailsReport = new ClientDetailsReport(_clientId, "Client Name", "Street", "123", "5000", "Bergen", "1234567890");
        _clientDetailsReports = new List<ClientDetailsReport> { _clientDetailsReport };
        OnDependency<IReportingRepository>()
            .Setup(x => x.GetByExample<ClientDetailsReport>(It.IsAny<object>()))
            .Returns(_clientDetailsReports);
    }

    protected override void Given()
    {
        Presenter.SetClient(new ClientReport(_clientId, "Client Name"));
        Presenter.Display();
    }

    protected override void When()
    {
        On<IClientDetailsView>().FireEvent(x => x.OnInitiateClientHasMoved += null);
    }

    [TestMethod]
    public void Then_the_menu_buttons_will_be_disabled()
    {
        On<IClientDetailsView>().VerifyThat.Method(x => x.DisableAddNewAccountMenu()).WasCalled();
        On<IClientDetailsView>().VerifyThat.Method(x => x.DisableClientHasMovedMenu()).WasCalled();
        On<IClientDetailsView>().VerifyThat.Method(x => x.DisableNameChangedMenu()).WasCalled();
        On<IClientDetailsView>().VerifyThat.Method(x => x.DisablePhoneNumberChangedMenu()).WasCalled();
    }

    [TestMethod]
    public void Then_the_name_change_panel_will_be_enabled()
    {
        On<IClientDetailsView>().VerifyThat.Method(x => x.EnableAddressPanel()).WasCalled();
    }
}