using System.Linq;
using Fohjin.EventStore;
using Fohjin.EventStore.Configuration;
using Fohjin.EventStore.Infrastructure;

namespace Test.Fohjin.EventStore.Infrastructure
{
    public class When_instantiating_an_aggregate_root_it_will_wire_the_events_correctly : BaseTestFixture
    {
        private TestClient _testClient;

        protected override void Given()
        {
            var eventProcessorCache = PreProcessorHelper.CreateEventProcessorCache();
            _testClient = new DomainRepository(new AggregateRootFactory(eventProcessorCache, new ApprovedEntitiesCache())).CreateNew<TestClient>();
        }

        protected override void When()
        {
            _testClient.ClientMoves(new Address("street", "number", "postalCode", "city"));
        }

        [Then]
        public void Then_the_internal_event_will_be_applied()
        {
            _testClient.GetAddress().Street.WillBe("street");
            _testClient.GetAddress().Number.WillBe("number");
            _testClient.GetAddress().PostalCode.WillBe("postalCode");
            _testClient.GetAddress().City.WillBe("city");
        }

        [Then]
        public void Then_the_internal_event_will_be_added_to_the_change_collection()
        {
            ((IEventProvider)_testClient).GetChanges().Last().WillBeOfType<ClientMovedEvent>();
        }
    }
}