using System;
using Fohjin.DDD.CommandHandlers;
using Fohjin.DDD.Commands;
using Fohjin.DDD.Domain.Account;
using Fohjin.DDD.Domain.Client;
using Fohjin.DDD.EventStore;
using Moq;

namespace Test.Fohjin.DDD.Scenarios.Client_wants_to_open_a_new_account
{
    public class When_opening_a_new_account_for_a_non_existing_client : CommandTestFixture<OpenNewAccountForClientCommand, OpenNewAccountForClientCommandHandler, Client>
    {
        protected override OpenNewAccountForClientCommand When()
        {
            return new OpenNewAccountForClientCommand(Guid.NewGuid(), "New Account");
        }

        [Then]
        public void Then_a_non_existing_client_exception_will_be_thrown()
        {
            CaughtException.WillBeOfType<NonExistingClientException>();
        }

        [Then]
        public void Then_the_exception_message_will_be()
        {
            CaughtException.Message.WillBe("The Client is not created and no opperations can be executed on it");
        }

        [Then]
        public void Then_there_is_no_new_account_to_be_saved()
        {
            OnDependency<IDomainRepository<IDomainEvent>>().Verify(x => x.Add(It.IsAny<ActiveAccount>()), Times.Never());
        }
    }
}