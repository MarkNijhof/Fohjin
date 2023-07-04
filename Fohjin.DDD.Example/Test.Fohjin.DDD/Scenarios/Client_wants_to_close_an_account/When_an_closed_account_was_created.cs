using Fohjin.DDD.EventHandlers;
using Fohjin.DDD.Events.Account;
using Fohjin.DDD.Reporting;
using Fohjin.DDD.Reporting.Dtos;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace Test.Fohjin.DDD.Scenarios.Client_wants_to_close_an_account;

public class When_an_closed_account_was_created : EventTestFixture<ClosedAccountCreatedEvent, ClosedAccountCreatedEventHandler>
{
    private static Guid _orginalAccountId;
    private static Guid _clientId;
    private ClosedAccountReport? SaveClosedAccountReportObject;
    private ClosedAccountDetailsReport? SaveClosedAccountDetailsReportObject;
    private List<KeyValuePair<string, string>>? ledgers;
    private Guid _accountId;

    protected override void SetupDependencies()
    {
        OnDependency<IReportingRepository>()
            .Setup(x => x.Save(It.IsAny<ClosedAccountReport>()))
            .Callback<ClosedAccountReport>(a => SaveClosedAccountReportObject = a);

        OnDependency<IReportingRepository>()
            .Setup(x => x.Save(It.IsAny<ClosedAccountDetailsReport>()))
            .Callback<ClosedAccountDetailsReport>(a => SaveClosedAccountDetailsReportObject = a);
    }

    protected override ClosedAccountCreatedEvent When()
    {
        _accountId = Guid.NewGuid();
        _orginalAccountId = Guid.NewGuid();
        _clientId = Guid.NewGuid();

        ledgers = new List<KeyValuePair<string, string>>
            {
                new KeyValuePair<string, string>("CreditMutation" , "10.5|"),
                new KeyValuePair<string, string>("DebitMutation" , "15.0|"),
                new KeyValuePair<string, string>("CreditTransfer" , "10.5|1234567890"),
                new KeyValuePair<string, string>("DebitTransfer" , "15.0|0987654321"),
                new KeyValuePair<string, string>("CreditTransferFailed" , "15.0|0987654321"),
            };

        return new ClosedAccountCreatedEvent(_accountId, _orginalAccountId, _clientId, ledgers, "Closed Account", "1234567890");
    }

    [TestMethod]
    public void Then_the_reporting_repository_will_be_used_to_save_the_closed_account_report()
    {
        OnDependency<IReportingRepository>().Verify(x => x.Save(It.IsAny<ClosedAccountReport>()));
    }

    [TestMethod]
    public void Then_the_reporting_repository_will_be_used_to_update_the_closed_account_report()
    {
        SaveClosedAccountReportObject?.Id.WillBe(_accountId);
        SaveClosedAccountReportObject?.ClientDetailsReportId.WillBe(_clientId);
        SaveClosedAccountReportObject?.AccountName.WillBe("Closed Account");
    }

    [TestMethod]
    public void Then_the_reporting_repository_will_be_used_to_save_the_closed_account_details_report()
    {
        OnDependency<IReportingRepository>().Verify(x => x.Save(It.IsAny<ClosedAccountDetailsReport>()));
    }

    [TestMethod]
    public void Then_the_reporting_repository_will_be_used_to_update_the_closed_account_details_report()
    {
        SaveClosedAccountDetailsReportObject?.Id.WillBe(_accountId);
        SaveClosedAccountDetailsReportObject?.ClientReportId.WillBe(_clientId);
        SaveClosedAccountDetailsReportObject?.Balance.WillBe(0);
        SaveClosedAccountDetailsReportObject?.AccountName.WillBe("Closed Account");
        SaveClosedAccountDetailsReportObject?.AccountNumber.WillBe("1234567890");
    }

    [TestMethod]
    public void Then_the_reporting_repository_will_be_used_to_save_the_four_ledger_reports()
    {
        OnDependency<IReportingRepository>().Verify(x => x.Save(It.IsAny<LedgerReport>()), Times.Exactly(5));
    }
}