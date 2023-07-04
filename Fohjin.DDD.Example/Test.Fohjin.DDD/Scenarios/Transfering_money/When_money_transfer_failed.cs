using Fohjin.DDD.EventHandlers;
using Fohjin.DDD.Events.Account;
using Fohjin.DDD.Reporting;
using Fohjin.DDD.Reporting.Dtos;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace Test.Fohjin.DDD.Scenarios.Transfering_money;

public class When_money_transfer_failed : EventTestFixture<MoneyTransferFailedEvent, MoneyTransferFailedEventHandler>
{
    private static Guid _accountId;
    private object? UpdateAccountDetailsObject;
    private object? WhereAccountDetailsObject;
    private LedgerReport? LedgerReportObject;

    protected override void SetupDependencies()
    {
        OnDependency<IReportingRepository>()
            .Setup(x => x.Update<AccountDetailsReport>(It.IsAny<object>(), It.IsAny<object>()))
            .Callback<object, object>((u, w) => { UpdateAccountDetailsObject = u; WhereAccountDetailsObject = w; });

        OnDependency<IReportingRepository>()
            .Setup(x => x.Save(It.IsAny<LedgerReport>()))
            .Callback<LedgerReport>(l => { LedgerReportObject = l; });
    }

    protected override MoneyTransferFailedEvent When()
    {
        _accountId = Guid.NewGuid();
        return new MoneyTransferFailedEvent(50.5M, 10.5M, "0987654321") { AggregateId = _accountId };
    }

    [TestMethod]
    public void Then_the_reporting_repository_will_be_used_to_update_the_account_details_report()
    {
        OnDependency<IReportingRepository>().Verify(x => x.Update<AccountDetailsReport>(It.IsAny<object>(), It.IsAny<object>()));
    }

    [TestMethod]
    public void Then_the_account_details_report_will_be_updated_with_the_expected_details()
    {
        UpdateAccountDetailsObject.WillBeSimuliar(new { Balance = 50.5M }.ToString() ?? "");
        WhereAccountDetailsObject.WillBeSimuliar(new { Id = _accountId });
    }

    [TestMethod]
    public void Then_the_reporting_repository_will_be_used_to_save_the_ledger_report()
    {
        OnDependency<IReportingRepository>().Verify(x => x.Save(It.IsAny<LedgerReport>()));
    }

    [TestMethod]
    public void Then_the_ledger_report_will_be_saved_with_the_expected_details()
    {
        LedgerReportObject?.AccountDetailsReportId.WillBe(_accountId);
        LedgerReportObject?.Amount.WillBe(10.5M);
        LedgerReportObject?.Action.WillBe("Transfer to 0987654321 failed");
    }
}