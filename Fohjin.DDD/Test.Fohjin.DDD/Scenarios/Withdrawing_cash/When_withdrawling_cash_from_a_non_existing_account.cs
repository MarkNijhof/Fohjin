using System;
using Fohjin.DDD.CommandHandlers;
using Fohjin.DDD.Commands;
using Fohjin.DDD.Domain.Account;

namespace Test.Fohjin.DDD.Scenarios.Withdrawing_cash
{
    public class When_withdrawling_cash_from_a_non_existing_account : CommandTestFixture<WithdrawlCashCommand, WithdrawlCashCommandHandler, ActiveAccount>
    {
        protected override WithdrawlCashCommand When()
        {
            return new WithdrawlCashCommand(Guid.NewGuid(), 0);
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