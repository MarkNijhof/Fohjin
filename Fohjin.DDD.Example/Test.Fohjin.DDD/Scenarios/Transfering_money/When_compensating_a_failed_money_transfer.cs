using Fohjin.DDD.CommandHandlers;
using Fohjin.DDD.Commands;
using Fohjin.DDD.Domain.Account;
using Fohjin.DDD.Events.Account;
using Fohjin.DDD.EventStore;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Test.Fohjin.DDD.Scenarios.Transfering_money;

public class When_compensating_a_failed_money_transfer : CommandTestFixture<MoneyTransferFailedCompensatingCommand, MoneyTransferFailedCompensatingCommandHandler, ActiveAccount>
{
    protected override IEnumerable<IDomainEvent> Given()
    {
        yield return PrepareDomainEvent.Set(new AccountOpenedEvent(Guid.NewGuid(), Guid.NewGuid(), "AccountName", "1234567890")).ToVersion(1);
        yield return PrepareDomainEvent.Set(new CashDepositedEvent(20, 20)).ToVersion(2);
        yield return PrepareDomainEvent.Set(new MoneyTransferSendEvent(15,5, "1234567890", "0987654321")).ToVersion(3);
    }

    protected override MoneyTransferFailedCompensatingCommand When() =>        new (Guid.NewGuid(), 5.0M, "0987654321");

    [TestMethod]
    public void Then_a_money_transfer_event_will_be_published()
    {
        PublishedEvents?.Last().WillBeOfType<MoneyTransferFailedEvent>();
    }

    [TestMethod]
    public void Then_the_published_event_will_contain_the_amount_and_new_account_balance()
    {
        PublishedEvents?.Last<MoneyTransferFailedEvent>().Amount.WillBe(5.0M);
        PublishedEvents?.Last<MoneyTransferFailedEvent>().Balance.WillBe(20.0M);
        PublishedEvents?.Last<MoneyTransferFailedEvent>().TargetAccount.WillBe("0987654321");
    }
}