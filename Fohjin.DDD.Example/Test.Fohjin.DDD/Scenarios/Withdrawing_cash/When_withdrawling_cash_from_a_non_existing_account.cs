using System;
using Fohjin.DDD.CommandHandlers;
using Fohjin.DDD.Commands;
using Fohjin.DDD.Domain.Account;

namespace Test.Fohjin.DDD.Scenarios.Withdrawing_cash
{
    public class When_Withdrawaling_cash_from_a_non_existing_account : CommandTestFixture<WithdrawalCashCommand, WithdrawalCashCommandHandler, ActiveAccount>
    {
        protected override WithdrawalCashCommand When()
        {
            return new WithdrawalCashCommand(Guid.NewGuid(), 0);
        }

        [TestMethod]
        public void Then_a_non_existing_account_exception_will_be_thrown()
        {
            CaughtException.WillBeOfType<NonExitsingAccountException>();
        }

        [TestMethod]
        public void Then_the_exception_message_will_be()
        {
            CaughtException.Message.WillBe("The ActiveAcount is not created and no opperations can be executed on it");
        }
    }
}