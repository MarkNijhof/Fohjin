using Fohjin.EventStore;
using Fohjin.EventStore.Infrastructure;

namespace Test.Fohjin.EventStore
{
    public class When_requesting_a_new_object_from_the_repository : BaseTestFixture
    {
        private TestObject CreatedObject;

        protected override void When()
        {
            CreatedObject = new DomainRepository(new AggregateRootFactory(new RegisteredEventsCache())).CreateNew<TestObject>();
        }

        [Then]
        public void The_created_object_will_not_be_null()
        {
            CreatedObject.WillNotBe(null);
        }

        [Then]
        public void The_created_object_will_be_of_the_requested_type()
        {
            CreatedObject.WillActLikeType<TestObject>();
        }

        [Then]
        public void The_created_object_will_implement_the_IEventProvider_interface()
        {
            CreatedObject.WillImplementInterface<IEventProvider>();
        }

        [Then]
        public void The_created_object_will_implement_the_IOrginator_interface()
        {
            CreatedObject.WillImplementInterface<IOrginator>();
        }
    }
}