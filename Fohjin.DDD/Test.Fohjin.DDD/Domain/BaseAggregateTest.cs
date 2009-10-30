using Fohjin.DDD.Domain;
using Fohjin.DDD.Events;

namespace Test.Fohjin.DDD.Domain
{
    public class When_sending_an_internal_event_that_is_not_registered : AggregateRootTestFixture<TestEntity>
    {
        protected override void When()
        {
            AggregateRoot.DoSomething();
        }

        [Then]
        public void Then_it_will_throw_an_exception()
        {
            CaughtException.WillBeOfType<DomainEventWasNotRegisteredException>();
        }

        [Then]
        public void Then_the_exception_will_have_the_following_message()
        {
            CaughtException.Message.WillBe(string.Format("The requested event '{0}' is not registered", typeof(SomeUnregisteredEvent).FullName));
        }
    }

    public class TestEntity : BaseAggregateRoot
    {
        public void DoSomething()
        {
            Apply(new SomeUnregisteredEvent());
        }
    }

    public class SomeUnregisteredEvent : DomainEvent
    {
    }
}