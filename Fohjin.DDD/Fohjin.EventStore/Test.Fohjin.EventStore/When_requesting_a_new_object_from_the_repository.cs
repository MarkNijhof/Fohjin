using Fohjin.EventStore;
using Fohjin.EventStore.Infrastructure;

namespace Test.Fohjin.EventStore
{
    public class When_requesting_a_new_object_from_the_repository : BaseTestFixture
    {
        private TestClient _createdClient;

        protected override void When()
        {
            _createdClient = new DomainRepository(new AggregateRootFactory(new RegisteredEventsCache())).CreateNew<TestClient>();
        }

        [Then]
        public void The_created_object_will_not_be_null()
        {
            _createdClient.WillNotBe(null);
        }

        [Then]
        public void The_created_object_will_be_of_the_requested_type()
        {
            _createdClient.WillActLikeType<TestClient>();
        }

        [Then]
        public void The_created_object_will_implement_the_IEventProvider_interface()
        {
            _createdClient.WillImplementInterface<IEventProvider>();
        }

        [Then]
        public void The_created_object_will_implement_the_IOrginator_interface()
        {
            _createdClient.WillImplementInterface<IOrginator>();
        }
    }
}