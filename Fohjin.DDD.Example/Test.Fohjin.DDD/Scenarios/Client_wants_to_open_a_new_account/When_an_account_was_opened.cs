using Fohjin.DDD.EventHandlers;
using Fohjin.DDD.Events.Account;
using Fohjin.DDD.Reporting;
using Fohjin.DDD.Reporting.Dtos;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace Test.Fohjin.DDD.Scenarios.Client_wants_to_open_a_new_account;

public class When_an_account_was_opened : EventTestFixture<AccountOpenedEvent, AccountOpenedEventHandler>
{
    private static Guid _clientId;
    private static Guid _accountId;
    private AccountReport? SaveAccountReportObject;
    private AccountDetailsReport? SaveAccountDetailsReportObject;

    protected override void SetupDependencies()
    {
        OnDependency<IReportingRepository>()
            .Setup(x => x.Save(It.IsAny<AccountReport>()))
            .Callback<AccountReport>(a => SaveAccountReportObject = a);

        OnDependency<IReportingRepository>()
            .Setup(x => x.Save(It.IsAny<AccountDetailsReport>()))
            .Callback<AccountDetailsReport>(a => SaveAccountDetailsReportObject = a);
    }

    protected override AccountOpenedEvent When()
    {
        _accountId = Guid.NewGuid();
        _clientId = Guid.NewGuid();
        return new AccountOpenedEvent(_accountId, _clientId, "New Account", "1234567890");
    }

    [TestMethod]
    public void Then_the_reporting_repository_will_be_used_to_update_the_account_report()
    {
        OnDependency<IReportingRepository>().Verify(x => x.Save(It.IsAny<AccountReport>()));
    }

    [TestMethod]
    public void Then_the_account_report_will_be_updated_with_the_expected_details()
    {
        SaveAccountReportObject?.Id.WillBe(_accountId);
        SaveAccountReportObject?.ClientDetailsReportId.WillBe(_clientId);
        SaveAccountReportObject?.AccountName.WillBe("New Account");
        SaveAccountReportObject?.AccountNumber.WillBe("1234567890");
    }

    [TestMethod]
    public void Then_the_reporting_repository_will_be_used_to_update_the_account_details_report()
    {
        OnDependency<IReportingRepository>().Verify(x => x.Save(It.IsAny<AccountDetailsReport>()));
    }

    [TestMethod]
    public void Then_the_account_details_report_will_be_updated_with_the_expected_details()
    {
        SaveAccountDetailsReportObject?.Id.WillBe(_accountId);
        SaveAccountDetailsReportObject?.ClientReportId.WillBe(_clientId);
        SaveAccountDetailsReportObject?.AccountName.WillBe("New Account");
        SaveAccountDetailsReportObject?.AccountNumber.WillBe("1234567890");
        SaveAccountDetailsReportObject?.Balance.WillBe(0.0M);
    }
}