using Fohjin.EventStore;
using Fohjin.EventStore.Configuration;
using Fohjin.EventStore.Infrastructure;

namespace Test.Fohjin.EventStore.Infrastructure
{
    public class When_updating_the_version : BaseTestFixture
    {
        private TestClient _testClient;

        protected override void Given()
        {
            var eventProcessorCache = PreProcessorHelper.CreateEventProcessorCache();
            _testClient = new DomainRepository(new AggregateRootFactory(eventProcessorCache, new ApprovedEntitiesCache())).CreateNew<TestClient>();
        }

        protected override void When()
        {
            var eventProvider = (IEventProvider)_testClient;
            eventProvider.UpdateVersion(10);
        }

        [Then]
        public void Then_the_version_will_be_updated()
        {
            var eventProvider = (IEventProvider) _testClient;
            eventProvider.Version.WillBe(10);
        }
    }
}