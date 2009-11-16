using System;
using Fohjin.DDD.CommandHandlers;
using Fohjin.DDD.Commands;
using Fohjin.DDD.Domain.Client;

namespace Test.Fohjin.DDD.Scenarios.Assign_new_bank_card
{
    public class When_assigning_a_new_bank_card_on_a_non_existing_client : CommandTestFixture<AssignNewBankCardCommand, AssignNewBankCardCommandHandler, Client>
    {
        private readonly Guid _accountId = Guid.NewGuid();
        private readonly Guid _clientId = Guid.NewGuid();

        protected override AssignNewBankCardCommand When()
        {
            return new AssignNewBankCardCommand(_clientId, _accountId);
        }

        [Then]
        public void Then_a_non_existing_account_exception_will_be_thrown()
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