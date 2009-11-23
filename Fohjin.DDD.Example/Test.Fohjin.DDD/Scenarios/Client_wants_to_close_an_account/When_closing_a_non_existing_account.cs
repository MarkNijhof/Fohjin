using System;
using Fohjin.DDD.CommandHandlers;
using Fohjin.DDD.Commands;
using Fohjin.DDD.Domain.Account;

namespace Test.Fohjin.DDD.Scenarios.Client_wants_to_close_an_account
{
    public class When_closing_a_non_existing_account : CommandTestFixture<CloseAccountCommand, CloseAccountCommandHandler, ActiveAccount>
    {
        protected override CloseAccountCommand When()
        {
            return new CloseAccountCommand(Guid.NewGuid());
        }

        [Then]
        public void Then_a_non_existing_account_exception_will_be_thrown()
        {
            CaughtException.WillBeOfType<NonExitsingAccountException>();
        }
    }
}