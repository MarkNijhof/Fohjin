using System;
using Fohjin.DDD.CommandHandlers;
using Fohjin.DDD.Commands;
using Fohjin.DDD.Domain.Account;

namespace Test.Fohjin.DDD.Scenarios.Depositing_cash
{
    public class When_depositing_cash_on_a_non_existing_account : CommandTestFixture<DepositeCashCommand, DepositeCashCommandHandler, ActiveAccount>
    {
        protected override DepositeCashCommand When()
        {
            return new DepositeCashCommand(Guid.NewGuid(), 0);
        }

        [Then]
        public void Then_a_non_existing_account_exception_will_be_thrown()
        {
            CaughtException.WillBeOfType<NonExitsingAccountException>();
        }

        [Then]
        public void Then_the_exception_message_will_be()
        {
            CaughtException.Message.WillBe("The ActiveAcount is not created and no opperations can be executed on it");
        }
    }
}