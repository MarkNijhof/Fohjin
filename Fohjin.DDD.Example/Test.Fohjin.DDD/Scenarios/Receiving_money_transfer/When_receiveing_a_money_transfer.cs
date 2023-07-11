using Fohjin.DDD.CommandHandlers;
using Fohjin.DDD.Commands;
using Fohjin.DDD.Domain.Account;
using Fohjin.DDD.Events.Account;
using Fohjin.DDD.EventStore;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Test.Fohjin.DDD.Scenarios.Receiving_money_transfer;

public class When_receiveing_a_money_transfer : CommandTestFixture<ReceiveMoneyTransferCommand, ReceiveMoneyTransferCommandHandler, ActiveAccount>
{
    protected override IEnumerable<IDomainEvent> Given()
    {
        yield return PrepareDomainEvent.Set(new AccountOpenedEvent(Guid.NewGuid(), Guid.NewGuid(), "AccountName", "1234567890")).ToVersion(1);
        yield return PrepareDomainEvent.Set(new CashDepositedEvent(20, 20)).ToVersion(1);
    }

    protected override ReceiveMoneyTransferCommand When() => new(Guid.NewGuid(), 5.0M, "0987654321");

    [TestMethod]
    public void Then_a_money_transfer_received_event_will_be_published()
    {
        PublishedEvents?.Last().WillBeOfType<MoneyTransferReceivedEvent>();
    }

    [TestMethod]
    public void Then_it_will_generate_an_Deposit_event_with_the_expected_ammount()
    {
        PublishedEvents?.Last<MoneyTransferReceivedEvent>().Amount.WillBe(5.0M);
        PublishedEvents?.Last<MoneyTransferReceivedEvent>().Balance.WillBe(25.0M);
        PublishedEvents?.Last<MoneyTransferReceivedEvent>().TargetAccount.WillBe("1234567890");
        PublishedEvents?.Last<MoneyTransferReceivedEvent>().SourceAccount.WillBe("0987654321");
    }
}