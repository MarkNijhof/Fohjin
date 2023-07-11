using Fohjin.DDD.BankApplication.Presenters;
using Fohjin.DDD.BankApplication.Views;
using Fohjin.DDD.Reporting;
using Fohjin.DDD.Reporting.Dtos;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace Test.Fohjin.DDD.Scenarios.Displaying_account_details;

public class When_in_the_GUI_displaying_account_details : PresenterTestFixture<AccountDetailsPresenter>
{
    private AccountDetailsReport _accountDetailsReport = null!;
    private List<AccountReport>? _accountReports;

    protected override void SetupDependencies()
    {
        _accountDetailsReport = new AccountDetailsReport(Guid.NewGuid(), Guid.NewGuid(), "Account name", 10.5M, "1234567890");
        var accountDetailsReports = new List<AccountDetailsReport> {_accountDetailsReport};

        OnDependency<IReportingRepository>()
            .Setup(x => x.GetByExample<AccountDetailsReport>(It.IsAny<object>()))
            .Returns(accountDetailsReports);

        var accountReport1 = new AccountReport(Guid.NewGuid(), Guid.NewGuid(), "Account name 1", "1234567890");
        var accountReport2 = new AccountReport(Guid.NewGuid(), Guid.NewGuid(), "Account name 2", "1234567890");
        _accountReports = new List<AccountReport> {accountReport1, accountReport2};

        OnDependency<IReportingRepository>()
            .Setup(x => x.GetByExample<AccountReport>(It.IsAny<object>()))
            .Returns(_accountReports);
    }

    protected override void When()
    {
        Presenter?.SetAccount(new AccountReport(Guid.NewGuid(), Guid.NewGuid(), "Account name", "1234567890"));
        Presenter?.Display();
    }

    [TestMethod]
    public void Then_the_save_button_will_be_disabled()
    {
        On<IAccountDetailsView>().VerifyThat.Method(x => x.DisableSaveButton()).WasCalled();
    }

    [TestMethod]
    public void Then_the_menu_button_will_be_enabled()
    {
        On<IAccountDetailsView>().VerifyThat.Method(x => x.EnableMenuButtons()).WasCalled();
    }

    [TestMethod]
    public void Then_overview_panel_will_be_shown()
    {
        On<IAccountDetailsView>().VerifyThat.Method(x => x.EnableDetailsPanel()).WasCalled();
    }

    [TestMethod]
    public void Then_client_details_report_data_from_the_reporting_repository_is_being_loaded_into_the_view()
    {
        On<IAccountDetailsView>().VerifyThat.ValueIsSetFor(x => x.AccountName = _accountDetailsReport.AccountName);
        On<IAccountDetailsView>().VerifyThat.ValueIsSetFor(x => x.AccountNameLabel = _accountDetailsReport.AccountName);
        On<IAccountDetailsView>().VerifyThat.ValueIsSetFor(x => x.AccountNumberLabel = _accountDetailsReport.AccountNumber);
        On<IAccountDetailsView>().VerifyThat.ValueIsSetFor(x => x.BalanceLabel = _accountDetailsReport.Balance);
        On<IAccountDetailsView>().VerifyThat.ValueIsSetFor(x => x.Ledgers = _accountDetailsReport.Ledgers);
        On<IAccountDetailsView>().VerifyThat.ValueIsSetFor(x => x.TransferAccounts = _accountReports);
    }

    [TestMethod]
    public void Then_show_dialog_will_be_called_on_the_view()
    {
        On<IAccountDetailsView>().VerifyThat.Method(x => x.ShowDialog()).WasCalled();
    }
}