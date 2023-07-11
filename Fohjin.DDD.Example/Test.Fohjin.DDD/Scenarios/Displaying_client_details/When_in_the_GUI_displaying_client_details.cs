using System;
using System.Collections.Generic;
using Fohjin.DDD.BankApplication.Presenters;
using Fohjin.DDD.BankApplication.Views;
using Fohjin.DDD.Reporting;
using Fohjin.DDD.Reporting.Dtos;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace Test.Fohjin.DDD.Scenarios.Displaying_client_details;

public class When_in_the_GUI_displaying_client_details : PresenterTestFixture<ClientDetailsPresenter>
{
    private readonly Guid _clientId = Guid.NewGuid();
    private ClientDetailsReport _clientDetailsReport = null!;
    private List<ClientDetailsReport> _clientDetailsReports = new();

    protected override void SetupDependencies()
    {
        _clientDetailsReport = new ClientDetailsReport(_clientId, "Client Name", "street", "123", "5000", "bergen", "1234567890");
        _clientDetailsReports = new List<ClientDetailsReport> { _clientDetailsReport };
        OnDependency<IReportingRepository>()
            .Setup(x => x.GetByExample<ClientDetailsReport>(It.IsAny<object>()))
            .Returns(_clientDetailsReports);
    }

    protected override void When()
    {
        Presenter.SetClient(new ClientReport(_clientId, "Client Name"));
        Presenter.Display();
    }

    [TestMethod]
    public void Then_the_save_button_will_be_disabled()
    {
        On<IClientDetailsView>().VerifyThat.Method(x => x.DisableSaveButton()).WasCalled();
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
    public void Then_overview_panel_will_be_shown()
    {
        On<IClientDetailsView>().VerifyThat.Method(x => x.EnableOverviewPanel()).WasCalled();
    }

    [TestMethod]
    public void Then_client_details_report_data_from_the_reporting_repository_is_being_loaded_into_the_view()
    {
        On<IClientDetailsView>().VerifyThat.ValueIsSetFor(x => x.ClientName = _clientDetailsReport.ClientName);
        On<IClientDetailsView>().VerifyThat.ValueIsSetFor(x => x.Street = _clientDetailsReport.Street);
        On<IClientDetailsView>().VerifyThat.ValueIsSetFor(x => x.StreetNumber = _clientDetailsReport.StreetNumber);
        On<IClientDetailsView>().VerifyThat.ValueIsSetFor(x => x.PostalCode = _clientDetailsReport.PostalCode);
        On<IClientDetailsView>().VerifyThat.ValueIsSetFor(x => x.City = _clientDetailsReport.City);
        On<IClientDetailsView>().VerifyThat.ValueIsSetFor(x => x.PhoneNumber = _clientDetailsReport.PhoneNumber);
        On<IClientDetailsView>().VerifyThat.ValueIsSetFor(x => x.Accounts = _clientDetailsReport.Accounts);
        On<IClientDetailsView>().VerifyThat.ValueIsSetFor(x => x.ClosedAccounts = _clientDetailsReport.ClosedAccounts);

        On<IClientDetailsView>().VerifyThat.ValueIsSetFor(x => x.ClientNameLabel = _clientDetailsReport.ClientName);
        On<IClientDetailsView>().VerifyThat.ValueIsSetFor(x => x.PhoneNumberLabel = _clientDetailsReport.PhoneNumber);
        On<IClientDetailsView>().VerifyThat.ValueIsSetFor(x => x.AddressLine1Label = string.Format("{0} {1}", _clientDetailsReport.Street, _clientDetailsReport.StreetNumber));
        On<IClientDetailsView>().VerifyThat.ValueIsSetFor(x => x.AddressLine2Label = string.Format("{0} {1}", _clientDetailsReport.PostalCode, _clientDetailsReport.City));
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
    public void Then_show_dialog_will_be_called_on_the_view()
    {
        On<IClientDetailsView>().VerifyThat.Method(x => x.ShowDialog()).WasCalled();
    }
}