using System;
using Fohjin.DDD.CommandHandlers;
using Fohjin.DDD.Commands;
using Fohjin.DDD.Domain.Client;

namespace Test.Fohjin.DDD.Scenarios.Client_got_his_name_changed
{
    public class When_changing_the_name_of_a_non_existing_client : CommandTestFixture<ChangeClientNameCommand, ChangeClientNameCommandHandler, Client>
    {
        protected override ChangeClientNameCommand When()
        {
            return new ChangeClientNameCommand(Guid.NewGuid(), "Mark Nijhof");
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
    }
}