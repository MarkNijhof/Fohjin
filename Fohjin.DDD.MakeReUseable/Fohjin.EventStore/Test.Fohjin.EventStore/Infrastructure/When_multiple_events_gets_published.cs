using System.Linq;
using Fohjin.EventStore;
using Fohjin.EventStore.Configuration;
using Fohjin.EventStore.Infrastructure;

namespace Test.Fohjin.EventStore.Infrastructure
{
    public class When_multiple_events_gets_published : BaseTestFixture
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
            _testClient.ClientMoves(new Address("abc", "123", "123", "abc"));
        }

        [Then]
        public void Then_the_internal_collection_of_events_will_contain_the_published_event()
        {
            var eventProvider = (IEventProvider) _testClient;
            eventProvider.GetChanges().Count().WillBe(2);
        }

        [Then]
        public void Then_the_first_published_event_will_be_first_in_the_collection()
        {
            var eventProvider = (IEventProvider) _testClient;
            eventProvider.GetChanges().First().As<ClientMovedEvent>().Address.Street.WillBe("street");
            eventProvider.GetChanges().First().As<ClientMovedEvent>().Address.Number.WillBe("number");
            eventProvider.GetChanges().First().As<ClientMovedEvent>().Address.PostalCode.WillBe("postalCode");
            eventProvider.GetChanges().First().As<ClientMovedEvent>().Address.City.WillBe("city");
        }

        [Then]
        public void Then_the_second_published_event_will_be_second_in_the_collection()
        {
            var eventProvider = (IEventProvider) _testClient;
            eventProvider.GetChanges().Last().As<ClientMovedEvent>().Address.Street.WillBe("abc");
            eventProvider.GetChanges().Last().As<ClientMovedEvent>().Address.Number.WillBe("123");
            eventProvider.GetChanges().Last().As<ClientMovedEvent>().Address.PostalCode.WillBe("123");
            eventProvider.GetChanges().Last().As<ClientMovedEvent>().Address.City.WillBe("abc");
        }
    }
}