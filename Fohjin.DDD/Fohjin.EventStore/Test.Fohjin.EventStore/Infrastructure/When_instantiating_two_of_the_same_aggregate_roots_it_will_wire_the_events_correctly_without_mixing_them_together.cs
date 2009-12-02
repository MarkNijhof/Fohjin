using System.Linq;
using Fohjin.EventStore;
using Fohjin.EventStore.Configuration;
using Fohjin.EventStore.Infrastructure;

namespace Test.Fohjin.EventStore.Infrastructure
{
    public class When_instantiating_two_of_the_same_aggregate_roots_it_will_wire_the_events_correctly_without_mixing_them_together : BaseTestFixture
    {
        private TestClient _testClient1;
        private TestClient _testClient2;

        protected override void Given()
        {
            var approvedEntitiesCache = new ApprovedEntitiesCache();
            var eventProcessorCache = PreProcessorHelper.CreateEventProcessorCache();
            _testClient1 = new DomainRepository(new AggregateRootFactory(eventProcessorCache, approvedEntitiesCache)).CreateNew<TestClient>();
            _testClient2 = new DomainRepository(new AggregateRootFactory(eventProcessorCache, approvedEntitiesCache)).CreateNew<TestClient>();
        }

        protected override void When()
        {
            _testClient1.ClientMoves(new Address("street", "1", "1111", "city"));
            _testClient2.ClientMoves(new Address("street", "2", "2222", "city"));
        }

        [Then]
        public void Then_the_internal_event_will_be_applied_1()
        {
            _testClient1.GetAddress().Street.WillBe("street");
            _testClient1.GetAddress().Number.WillBe("1");
            _testClient1.GetAddress().PostalCode.WillBe("1111");
            _testClient1.GetAddress().City.WillBe("city");
        }

        [Then]
        public void Then_the_internal_event_will_be_applied_2()
        {
            _testClient2.GetAddress().Street.WillBe("street");
            _testClient2.GetAddress().Number.WillBe("2");
            _testClient2.GetAddress().PostalCode.WillBe("2222");
            _testClient2.GetAddress().City.WillBe("city");
        }

        [Then]
        public void Then_the_internal_event_will_be_added_to_the_change_collection_1()
        {
            ((IEventProvider)_testClient1).GetChanges().Last().WillBeOfType<ClientMovedEvent>();
        }

        [Then]
        public void Then_the_internal_event_will_be_added_to_the_change_collection_2()
        {
            ((IEventProvider)_testClient2).GetChanges().Last().WillBeOfType<ClientMovedEvent>();
        }
    }
}