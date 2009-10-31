using System;
using Fohjin.DDD.CommandHandlers;
using Fohjin.DDD.Commands;
using Fohjin.DDD.Domain.Client;

namespace Test.Fohjin.DDD.Scenarios.Client_moved
{
    public class When_a_non_existing_client_is_moving : CommandTestFixture<ClientIsMovingCommand, ClientIsMovingCommandHandler, Client>
    {
        protected override ClientIsMovingCommand When()
        {
            return new ClientIsMovingCommand(Guid.NewGuid(), "Welhavens gate", "49b", "5006", "Bergen");
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