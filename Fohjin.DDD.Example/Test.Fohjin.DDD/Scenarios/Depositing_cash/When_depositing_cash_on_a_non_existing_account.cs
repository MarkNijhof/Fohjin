using System;
using Fohjin.DDD.CommandHandlers;
using Fohjin.DDD.Commands;
using Fohjin.DDD.Domain.Account;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Test.Fohjin.DDD.Scenarios.Depositing_cash
{
    public class When_depositing_cash_on_a_non_existing_account : CommandTestFixture<DepositCashCommand, DepositCashCommandHandler, ActiveAccount>
    {
        protected override DepositCashCommand When()
        {
            return new DepositCashCommand(Guid.NewGuid(), 0);
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