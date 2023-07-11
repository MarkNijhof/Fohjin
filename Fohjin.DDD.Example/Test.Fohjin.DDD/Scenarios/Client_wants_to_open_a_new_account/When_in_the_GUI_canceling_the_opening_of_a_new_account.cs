using Fohjin.DDD.BankApplication.Presenters;
using Fohjin.DDD.BankApplication.Views;
using Fohjin.DDD.Reporting;
using Fohjin.DDD.Reporting.Dtos;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace Test.Fohjin.DDD.Scenarios.Client_wants_to_open_a_new_account;

public class When_in_the_GUI_canceling_the_opening_of_a_new_account : PresenterTestFixture<ClientDetailsPresenter>
{
    private readonly Guid _clientId = Guid.NewGuid();
    private ClientDetailsReport _clientDetailsReport = null!;
    private List<ClientDetailsReport>? _clientDetailsReports;

    protected override void SetupDependencies()
    {
        _clientDetailsReport = new ClientDetailsReport(_clientId, "Client Name", "street", "123", "5000", "bergen", "1234567890");
        _clientDetailsReports = new List<ClientDetailsReport> { _clientDetailsReport };
        OnDependency<IReportingRepository>()
            ?.Setup(x => x.GetByExample<ClientDetailsReport>(It.IsAny<object>()))
            .Returns(_clientDetailsReports);
    }

    protected override void Given()
    {
        On<IClientDetailsView>().FireEvent(x => x.OnInitiateOpenNewAccount += delegate { });
        On<IClientDetailsView>().ValueFor(x => x.NewAccountName).IsSetTo("New account name");
        On<IClientDetailsView>().FireEvent(x => x.OnFormElementGotChanged += null);
    }

    protected override void When()
    {
        On<IClientDetailsView>().FireEvent(x => x.OnCancel += null);
    }

    [TestMethod]
    public void Then_the_menu_buttons_will_be_enabled()
    {
        On<IClientDetailsView>().VerifyThat.Method(x => x.EnableAddNewAccountMenu()).WasCalled();
        On<IClientDetailsView>().VerifyThat.Method(x => x.EnableClientHasMovedMenu()).WasCalled();
        On<IClientDetailsView>().VerifyThat.Method(x => x.EnableNameChangedMenu()).WasCalled();
        On<IClientDetailsView>().VerifyThat.Method(x => x.EnablePhoneNumberChangedMenu()).WasCalled();
    }

    [TestMethod]
    public void Then_disable_the_save_button()
    {
        On<IClientDetailsView>().VerifyThat.Method(x => x.DisableSaveButton()).WasCalled();
    }

    [TestMethod]
    public void Then_overview_panel_will_be_shown()
    {
        On<IClientDetailsView>().VerifyThat.Method(x => x.EnableOverviewPanel()).WasCalled();
    }
}