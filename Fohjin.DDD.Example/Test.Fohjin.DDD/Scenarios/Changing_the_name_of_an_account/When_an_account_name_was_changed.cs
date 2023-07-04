using Fohjin.DDD.EventHandlers;
using Fohjin.DDD.Events.Account;
using Fohjin.DDD.Reporting;
using Fohjin.DDD.Reporting.Dtos;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace Test.Fohjin.DDD.Scenarios.Changing_the_name_of_an_account;

public class When_an_account_name_was_changed : EventTestFixture<AccountNameChangedEvent, AccountNameChangedEventHandler>
{
    private static Guid _accountId;
    private object? UpdateAccountObject;
    private object? WhereAccountObject;
    private object? UpdateAccountDetailsObject;
    private object? WhereAccountDetailsObject;

    protected override void SetupDependencies()
    {
        OnDependency<IReportingRepository>()
            ?.Setup(x => x.Update<AccountReport>(It.IsAny<object>(), It.IsAny<object>()))
            .Callback<object, object>((u, w) => { UpdateAccountObject = u; WhereAccountObject = w; });

        OnDependency<IReportingRepository>()
            ?.Setup(x => x.Update<AccountDetailsReport>(It.IsAny<object>(), It.IsAny<object>()))
            .Callback<object, object>((u, w) => { UpdateAccountDetailsObject = u; WhereAccountDetailsObject = w; });
    }

    protected override AccountNameChangedEvent When()
    {
        var accountNameGotChangedEvent = new AccountNameChangedEvent("New Account Name") { AggregateId = Guid.NewGuid() };
        _accountId = accountNameGotChangedEvent.AggregateId;
        return accountNameGotChangedEvent;
    }

    [TestMethod]
    public void Then_the_reporting_repository_will_be_used_to_update_the_client_report()
    {
        OnDependency<IReportingRepository>().Verify(x => x.Update<AccountReport>(It.IsAny<object>(), It.IsAny<object>()));
    }

    [TestMethod]
    public void Then_the_account_report_will_be_updated_with_the_expected_details()
    {
        UpdateAccountObject.WillBeSimuliar(new { AccountName = "New Account Name" }.ToString() ?? "");
        WhereAccountObject.WillBeSimuliar(new { Id = _accountId });
    }

    [TestMethod]
    public void Then_the_reporting_repository_will_be_used_to_update_the_client_details_report()
    {
        OnDependency<IReportingRepository>().Verify(x => x.Update<AccountDetailsReport>(It.IsAny<object>(), It.IsAny<object>()));
    }

    [TestMethod]
    public void Then_the_account_details_report_will_be_updated_with_the_expected_details()
    {
        UpdateAccountDetailsObject.WillBeSimuliar(new { AccountName = "New Account Name" }.ToString() ?? "");
        WhereAccountDetailsObject.WillBeSimuliar(new { Id = _accountId });
    }
}