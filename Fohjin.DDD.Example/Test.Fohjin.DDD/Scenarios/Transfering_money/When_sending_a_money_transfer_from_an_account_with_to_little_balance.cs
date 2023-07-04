using Fohjin.DDD.CommandHandlers;
using Fohjin.DDD.Commands;
using Fohjin.DDD.Domain.Account;
using Fohjin.DDD.Events.Account;
using Fohjin.DDD.EventStore;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Test.Fohjin.DDD.Scenarios.Transfering_money
{
    public class When_sending_a_money_transfer_from_an_account_with_to_little_balance : CommandTestFixture<SendMoneyTransferCommand, SendMoneyTransferCommandHandler, ActiveAccount>
    {
        protected override IEnumerable<IDomainEvent> Given()
        {
            yield return PrepareDomainEvent.Set(new AccountOpenedEvent(Guid.NewGuid(), Guid.NewGuid(), "AccountName", "1234567890")).ToVersion(1);
        }

        protected override SendMoneyTransferCommand When()
        {
            return new SendMoneyTransferCommand(Guid.NewGuid(), 10.5M, "1234567890");
        }

        [TestMethod]
        public void Then_an_account_balance_to_low_exception_will_be_thrown()
        {
            CaughtException.WillBeOfType<AccountBalanceToLowException>();
        }

        [TestMethod]
        public void Then_the_exception_message_will_be()
        {
            CaughtException?.WithMessage(string.Format("The amount {0:C} is larger than your current balance {1:C}", 10.5M, 0));
        }
    }
}