using System.Linq;
using Fohjin.EventStore;
using Fohjin.EventStore.Infrastructure;

namespace Test.Fohjin.EventStore
{
    public class When_executing_some_behavior_on_an_aggregate_root : BaseTestFixture
    {
        private TestObject CreatedObject;

        protected override void Given()
        {
            CreatedObject = new DomainRepository(new AggregateRootFactory(new RegisteredEventsCache())).CreateNew<TestObject>();
        }

        protected override void When()
        {
            CreatedObject.DoSomething("value");
        }

        [Then]
        public void Then_the_internal_event_will_be_applied()
        {
            CreatedObject.GetValue().WillBe("value");
        }

        [Then]
        public void Then_the_internal_event_will_be_added_to_the_change_collection()
        {
            ((IEventProvider)CreatedObject).GetChanges().Last().WillBeOfType<SomeEvent>();
        }
    }
}