using Fohjin.DDD.CommandHandlers;
using Fohjin.DDD.Commands;
using Fohjin.DDD.Domain.Account;
using Fohjin.DDD.Events.Account;
using Fohjin.DDD.EventStore;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Test.Fohjin.DDD.Scenarios.Transfering_money;

public class When_sending_a_money_transfer : CommandTestFixture<SendMoneyTransferCommand, SendMoneyTransferCommandHandler, ActiveAccount>
{
    protected override IEnumerable<IDomainEvent> Given()
    {
        yield return PrepareDomainEvent.Set(new AccountOpenedEvent(Guid.NewGuid(), Guid.NewGuid(), "AccountName", "1234567890")).ToVersion(1);
        yield return PrepareDomainEvent.Set(new CashDepositedEvent(20, 20)).ToVersion(1);
    }

    protected override SendMoneyTransferCommand When() => new(Guid.NewGuid(), 5.0M, "1234567890");

    [TestMethod]
    public void Then_a_money_transfer_event_will_be_published()
    {
        PublishedEvents?.Last().WillBeOfType<MoneyTransferSendEvent>();
    }

    [TestMethod]
    public void Then_the_published_event_will_contain_the_amount_and_new_account_balance()
    {
        PublishedEvents?.Last<MoneyTransferSendEvent>().Amount.WillBe(5.0M);
        PublishedEvents?.Last<MoneyTransferSendEvent>().Balance.WillBe(15.0M);
        PublishedEvents?.Last<MoneyTransferSendEvent>().TargetAccount.WillBe("1234567890");
    }
}