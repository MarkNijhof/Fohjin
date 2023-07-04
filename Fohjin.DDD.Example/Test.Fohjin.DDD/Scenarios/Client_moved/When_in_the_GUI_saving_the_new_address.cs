using Fohjin.DDD.BankApplication.Presenters;
using Fohjin.DDD.BankApplication.Views;
using Fohjin.DDD.Bus;
using Fohjin.DDD.Commands;
using Fohjin.DDD.Reporting;
using Fohjin.DDD.Reporting.Dtos;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace Test.Fohjin.DDD.Scenarios.Client_moved;

public class When_in_the_GUI_saving_the_new_address : PresenterTestFixture<ClientDetailsPresenter>
{
    private readonly Guid _clientId = Guid.NewGuid();
    private ClientDetailsReport _clientDetailsReport = null!;
    private List<ClientDetailsReport>? _clientDetailsReports;

    protected override void SetupDependencies()
    {
        OnDependency<IPopupPresenter>()
            .Setup(x => x.CatchPossibleException(It.IsAny<Action>()))
            .Callback<Action>(x => x());

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
        On<IClientDetailsView>().ValueFor(x => x.ClientName).IsSetTo("Client name");
        On<IClientDetailsView>().ValueFor(x => x.PhoneNumber).IsSetTo("1234567890");
        On<IClientDetailsView>().ValueFor(x => x.Street).IsSetTo("Lane");
        On<IClientDetailsView>().ValueFor(x => x.StreetNumber).IsSetTo("321");
        On<IClientDetailsView>().ValueFor(x => x.PostalCode).IsSetTo("6000");
        On<IClientDetailsView>().ValueFor(x => x.City).IsSetTo("Oslo");
        On<IClientDetailsView>().FireEvent(x => x.OnFormElementGotChanged += null);
        On<IClientDetailsView>().FireEvent(x => x.OnInitiateClientPhoneNumberChanged += null);
    }

    protected override void When()
    {
        On<IClientDetailsView>().FireEvent(x => x.OnSaveNewAddress += null);
    }

    [TestMethod]
    public void Then_a_change_client_phone_number_command_will_be_published()
    {
        On<IBus>().VerifyThat.Method(x => x.Publish(It.IsAny<ClientIsMovingCommand>())).WasCalled();
    }

    [TestMethod]
    public void Then_the_save_button_will_be_disabled()
    {
        On<IClientDetailsView>().VerifyThat.Method(x => x.DisableSaveButton()).WasCalled();
    }

    [TestMethod]
    public void Then_the_menu_button_will_be_enabled()
    {
        On<IClientDetailsView>().VerifyThat.Method(x => x.EnableAddNewAccountMenu()).WasCalled();
        On<IClientDetailsView>().VerifyThat.Method(x => x.EnableClientHasMovedMenu()).WasCalled();
        On<IClientDetailsView>().VerifyThat.Method(x => x.EnableNameChangedMenu()).WasCalled();
        On<IClientDetailsView>().VerifyThat.Method(x => x.EnablePhoneNumberChangedMenu()).WasCalled();
    }

    [TestMethod]
    public void Then_the_details_panel_will_be_enabled()
    {
        On<IClientDetailsView>().VerifyThat.Method(x => x.EnableOverviewPanel()).WasCalled();
    }
}