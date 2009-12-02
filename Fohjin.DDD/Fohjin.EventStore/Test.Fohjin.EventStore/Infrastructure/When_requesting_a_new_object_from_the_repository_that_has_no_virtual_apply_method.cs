using Fohjin.EventStore;
using Fohjin.EventStore.Configuration;
using Fohjin.EventStore.Infrastructure;

namespace Test.Fohjin.EventStore.Infrastructure
{
    public class When_requesting_a_new_object_from_the_repository_that_has_no_proteced_virtual_apply_method_declared : BaseTestFixture
    {
        protected override void When()
        {
            new DomainRepository(new AggregateRootFactory(new EventProcessorCache(), new ApprovedEntitiesCache())).CreateNew<TestObjectWithoutApplyMethod>();
        }

        [Then]
        public void The_an_exception_will_be_thrown()
        {
            CaughtException.WillBeOfType<MethodMissingException>();
        }

        [Then]
        public void The_exception_message_will_be()
        {
            CaughtException.Message.WillBe(string.Format("Object '{0}' needs to have a 'proteced virtual void Apply(object @event)' method declared", typeof(TestObjectWithoutApplyMethod).FullName));
        }
    }

    public class TestObjectWithoutApplyMethod
    {
    }
}