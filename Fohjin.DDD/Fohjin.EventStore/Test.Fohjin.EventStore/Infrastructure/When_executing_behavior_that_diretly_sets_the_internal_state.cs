using Fohjin.EventStore;
using Fohjin.EventStore.Configuration;
using Fohjin.EventStore.Infrastructure;

namespace Test.Fohjin.EventStore.Infrastructure
{
    public class When_executing_behavior_that_diretly_sets_the_internal_state : BaseTestFixture
    {
        private TestClient _testClient;

        protected override void Given()
        {
            _testClient = new DomainRepository(new AggregateRootFactory(new EventProcessorCache(), new ApprovedEntitiesCache())).CreateNew<TestClient>();
        }

        protected override void When()
        {
            _testClient.ClientMovesIlligalAction(new Address("street", "123", "5000", "Bergen"));
        }

        [Then]
        public void The_an_exception_will_be_thrown()
        {
            CaughtException.WillBeOfType<IlligalStateAssignmentException>();
        }

        [Then]
        public void The_exception_message_will_be()
        {
            CaughtException.Message.WillBe(string.Format("Internal state is not allowed to be altered directly using property '{0}' and should always be done through the publishing of an event!", "Address"));
        }
    }
}