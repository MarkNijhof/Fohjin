using System.Linq;
using Fohjin.EventStore;
using Fohjin.EventStore.Infrastructure;

namespace Test.Fohjin.EventStore
{
    public class When_instantiating_an_aggregate_root_it_will_wire_the_events_correctly : BaseTestFixture
    {
        private TestClient _createdClient;

        protected override void Given()
        {
            _createdClient = new DomainRepository(new AggregateRootFactory(new EventRegistrator(), new RegisteredEventsCache())).CreateNew<TestClient>();
        }

        protected override void When()
        {
            _createdClient.ClientMoves(new Address("street", "number", "postalCode", "city"));
        }

        [Then]
        public void Then_the_internal_event_will_be_applied()
        {
            _createdClient.GetAddress().Street.WillBe("street");
            _createdClient.GetAddress().Number.WillBe("number");
            _createdClient.GetAddress().PostalCode.WillBe("postalCode");
            _createdClient.GetAddress().City.WillBe("city");
        }

        [Then]
        public void Then_the_internal_event_will_be_added_to_the_change_collection()
        {
            ((IEventProvider)_createdClient).GetChanges().Last().WillBeOfType<ClientMovedEvent>();
        }
    }
}