using Fohjin.EventStore;
using Fohjin.EventStore.Configuration;
using Fohjin.EventStore.Infrastructure;

namespace Test.Fohjin.EventStore.Infrastructure
{
    public class When_requesting_a_new_object_from_the_repository : BaseTestFixture
    {
        private TestClient _testClient;

        protected override void When()
        {
            _testClient = new DomainRepository(new AggregateRootFactory(new EventProcessorCache(), new ApprovedEntitiesCache())).CreateNew<TestClient>();
        }

        [Then]
        public void The_created_object_will_not_be_null()
        {
            _testClient.WillNotBe(null);
        }

        [Then]
        public void The_created_object_will_be_of_the_requested_type()
        {
            _testClient.WillActLikeType<TestClient>();
        }

        [Then]
        public void The_created_object_will_implement_the_IEventProvider_interface()
        {
            _testClient.WillImplementInterface<IEventProvider>();
        }

        [Then]
        public void The_created_object_will_implement_the_IOrginator_interface()
        {
            _testClient.WillImplementInterface<IOrginator>();
        }
    }
}