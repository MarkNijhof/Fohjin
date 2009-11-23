using System;
using System.Collections.Generic;
using Fohjin.DDD.CommandHandlers;
using Fohjin.DDD.Commands;
using Fohjin.DDD.Domain.Account;
using Fohjin.DDD.Events.Account;
using Fohjin.DDD.EventStore;

namespace Test.Fohjin.DDD.Scenarios.Depositing_cash
{
    public class When_depositing_cash_on_a_closed_account : CommandTestFixture<DepositeCashCommand, DepositeCashCommandHandler, ActiveAccount>
    {
        protected override IEnumerable<IDomainEvent> Given()
        {
            yield return PrepareDomainEvent.Set(new AccountOpenedEvent(Guid.NewGuid(), Guid.NewGuid(), "AccountName", "1234567890")).ToVersion(1);
            yield return PrepareDomainEvent.Set(new AccountClosedEvent()).ToVersion(2);
        }

        protected override DepositeCashCommand When()
        {
            return new DepositeCashCommand(Guid.NewGuid(), 0);
        }

        [Then]
        public void Then_a_closed_account_exception_will_be_thrown()
        {
            CaughtException.WillBeOfType<ClosedAccountException>();
        }

        [Then]
        public void Then_the_exception_message_will_be()
        {
            CaughtException.Message.WillBe("The ActiveAcount is closed and no opperations can be executed on it");
        }
    }
}