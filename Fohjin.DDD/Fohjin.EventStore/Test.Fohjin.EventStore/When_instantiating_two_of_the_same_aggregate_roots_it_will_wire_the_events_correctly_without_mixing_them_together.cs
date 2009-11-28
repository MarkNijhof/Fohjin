using System.Linq;
using Fohjin.EventStore;
using Fohjin.EventStore.Infrastructure;

namespace Test.Fohjin.EventStore
{
    public class When_instantiating_two_of_the_same_aggregate_roots_it_will_wire_the_events_correctly_without_mixing_them_together : BaseTestFixture
    {
        private TestClient _createdClient1;
        private TestClient _createdClient2;

        protected override void Given()
        {
            var registeredEventsCache = new RegisteredEventsCache();
            _createdClient1 = new DomainRepository(new AggregateRootFactory(new EventRegistrator(), registeredEventsCache)).CreateNew<TestClient>();
            _createdClient2 = new DomainRepository(new AggregateRootFactory(new EventRegistrator(), registeredEventsCache)).CreateNew<TestClient>();
        }

        protected override void When()
        {
            _createdClient1.ClientMoves(new Address("street", "1", "1111", "city"));
            _createdClient2.ClientMoves(new Address("street", "2", "2222", "city"));
        }

        [Then]
        public void Then_the_internal_event_will_be_applied_1()
        {
            _createdClient1.GetAddress().Street.WillBe("street");
            _createdClient1.GetAddress().Number.WillBe("1");
            _createdClient1.GetAddress().PostalCode.WillBe("1111");
            _createdClient1.GetAddress().City.WillBe("city");
        }

        [Then]
        public void Then_the_internal_event_will_be_applied_2()
        {
            _createdClient2.GetAddress().Street.WillBe("street");
            _createdClient2.GetAddress().Number.WillBe("2");
            _createdClient2.GetAddress().PostalCode.WillBe("2222");
            _createdClient2.GetAddress().City.WillBe("city");
        }

        [Then]
        public void Then_the_internal_event_will_be_added_to_the_change_collection_1()
        {
            ((IEventProvider)_createdClient1).GetChanges().Last().WillBeOfType<ClientMovedEvent>();
        }

        [Then]
        public void Then_the_internal_event_will_be_added_to_the_change_collection_2()
        {
            ((IEventProvider)_createdClient2).GetChanges().Last().WillBeOfType<ClientMovedEvent>();
        }
    }
}