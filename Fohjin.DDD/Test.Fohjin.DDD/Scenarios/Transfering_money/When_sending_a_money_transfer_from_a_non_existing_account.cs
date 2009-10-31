using System;
using Fohjin.DDD.CommandHandlers;
using Fohjin.DDD.Commands;
using Fohjin.DDD.Domain.Account;

namespace Test.Fohjin.DDD.Scenarios.Transfering_money
{
    public class When_sending_a_money_transfer_from_a_non_existing_account : CommandTestFixture<SendMoneyTransferCommand, SendMoneyTransferCommandHandler, ActiveAccount>
    {
        protected override SendMoneyTransferCommand When()
        {
            return new SendMoneyTransferCommand(Guid.NewGuid(), 10.0M, "1234567890");
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