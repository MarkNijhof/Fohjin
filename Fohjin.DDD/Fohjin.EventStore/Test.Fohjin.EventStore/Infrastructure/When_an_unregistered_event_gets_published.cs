using Fohjin.EventStore;
using Fohjin.EventStore.Configuration;
using Fohjin.EventStore.Infrastructure;

namespace Test.Fohjin.EventStore.Infrastructure
{
    public class When_an_unregistered_event_gets_published : BaseTestFixture
    {
        private TestClient _testClient;

        protected override void Given()
        {
            _testClient = new DomainRepository(new AggregateRootFactory(new EventProcessorCache(), new ApprovedEntitiesCache())).CreateNew<TestClient>();
        }

        protected override void When()
        {
            _testClient.ClientMoves(new Address("street", "number", "postalCode", "city"));
        }

        [Then]
        public void The_an_exception_will_be_thrown()
        {
            CaughtException.WillBeOfType<UnregisteredDomainEventException>();
        }

        [Then]
        public void The_exception_message_will_be()
        {
            CaughtException.Message.WillBe(string.Format("The requested class '{0}' is not registered as a domain event", typeof(ClientMovedEvent).FullName));
        }
    }
}