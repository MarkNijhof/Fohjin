using Fohjin.DDD.EventHandlers;
using Fohjin.DDD.Events.Account;
using Fohjin.DDD.Services;
using Fohjin.DDD.Services.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace Test.Fohjin.DDD.Scenarios.Transfering_money;

public class When_money_transfer_was_send_further : EventTestFixture<MoneyTransferSendEvent, SendMoneyTransferFurtherEventHandler>
{
    private static Guid _accountId;
    private MoneyTransfer MoneyTransfer = null!;

    protected override void SetupDependencies()
    {
        OnDependency<ISendMoneyTransfer>()
            .Setup(x => x.Send(It.IsAny<MoneyTransfer>()))
            .Callback<MoneyTransfer>(x => MoneyTransfer = x);
    }

    protected override MoneyTransferSendEvent When()
    {
        _accountId = Guid.NewGuid();
        return new (50.5M, 10.5M, "0987654321", "1234567890") { AggregateId = _accountId };
    }

    [TestMethod]
    public void Then_the_money_transfer_will_be_send_through_the_money_transfer_service()
    {
        OnDependency<ISendMoneyTransfer>().Verify(x => x.Send(It.IsAny<MoneyTransfer>()));
    }

    [TestMethod]
    public void Then_the_money_transfer_will_have_the_expected_details()
    {
        MoneyTransfer.Amount.WillBe(10.5M);
        MoneyTransfer.SourceAccount.WillBe("0987654321");
        MoneyTransfer.TargetAccount.WillBe("1234567890");
    }
}