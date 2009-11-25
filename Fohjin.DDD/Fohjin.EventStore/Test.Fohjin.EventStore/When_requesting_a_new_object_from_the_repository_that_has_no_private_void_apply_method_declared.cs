using Fohjin.EventStore;
using Fohjin.EventStore.Infrastructure;

namespace Test.Fohjin.EventStore
{
    public class When_requesting_a_new_object_from_the_repository_that_has_no_private_void_apply_method_declared : BaseTestFixture
    {
        protected override void When()
        {
            new DomainRepository(new AggregateRootFactory(new RegisteredEventsCache())).CreateNew<TestObjectWithoutProtectedRegisteredEventsMethod>();
        }

        [Then]
        public void The_an_exception_will_be_thrown()
        {
            CaughtException.WillBeOfType<ProtectedRegisteredEventsMethodMissingException>();
        }

        [Then]
        public void The_exception_message_will_be()
        {
            CaughtException.Message.WillBe(string.Format("Object '{0}' needs to have a 'protected IEnumerable<Type> RegisteredEvents()' method declared", typeof(TestObjectWithoutProtectedRegisteredEventsMethod).FullName));
        }
    }

    public class TestObjectWithoutProtectedRegisteredEventsMethod
    {
        protected virtual void Apply(object @event)
        {
        }
    }
}