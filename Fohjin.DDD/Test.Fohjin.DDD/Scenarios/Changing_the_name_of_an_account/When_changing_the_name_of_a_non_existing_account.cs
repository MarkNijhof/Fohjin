using System;
using Fohjin.DDD.CommandHandlers;
using Fohjin.DDD.Commands;
using Fohjin.DDD.Domain.Account;

namespace Test.Fohjin.DDD.Scenarios.Changing_the_name_of_an_account
{
    public class When_changing_the_name_of_a_non_existing_account : CommandTestFixture<ChangeAccountNameCommand, ChangeAccountNameCommandHandler, ActiveAccount>
    {
        protected override ChangeAccountNameCommand When()
        {
            return new ChangeAccountNameCommand(Guid.NewGuid(), "New Account Name");
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