using System;
using Fohjin.DDD.EventHandlers;
using Fohjin.DDD.Events.Client;

namespace Test.Fohjin.DDD.Scenarios.Client_wants_to_open_a_new_account
{
    public class When_an_account_was_assigned_to_a_client : EventTestFixture<AccountToClientAssignedEvent, AccountToClientAssignedEventHandler>
    {
        protected override AccountToClientAssignedEvent When()
        {
            return new AccountToClientAssignedEvent(Guid.NewGuid()) { AggregateId = Guid.NewGuid() };
        }

        [Then]
        public void Then_it_will_not_throw_an_exception()
        {
            CaughtException.WillBeOfType<ThereWasNoExceptionButOneWasExpectedException>();
        }
    }
}