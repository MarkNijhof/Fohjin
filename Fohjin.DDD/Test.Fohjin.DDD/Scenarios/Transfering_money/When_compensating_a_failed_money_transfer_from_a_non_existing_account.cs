using System;
using Fohjin.DDD.CommandHandlers;
using Fohjin.DDD.Commands;
using Fohjin.DDD.Domain.Account;

namespace Test.Fohjin.DDD.Scenarios.Transfering_money
{
    public class When_compensating_a_failed_money_transfer_from_a_non_existing_account : CommandTestFixture<MoneyTransferFailedCompensatingCommand, MoneyTransferFailedCompensatingCommandHandler, ActiveAccount>
    {
        protected override MoneyTransferFailedCompensatingCommand When()
        {
            return new MoneyTransferFailedCompensatingCommand(Guid.NewGuid(), 5.0M, "0987654321");
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