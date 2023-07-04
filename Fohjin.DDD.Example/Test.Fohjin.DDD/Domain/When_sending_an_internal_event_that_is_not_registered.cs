using Fohjin.DDD.EventStore.Aggregate;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Test.Fohjin.DDD.Domain
{
    public class When_sending_an_internal_event_that_is_not_registered : AggregateRootTestFixture<TestAggregateRoot>
    {
        protected override void When()
        {
            AggregateRoot?.DoSomethingIlligal();
        }

        [TestMethod]
        public void Then_it_will_throw_an_unregistered_domain_event_exception()
        {
            CaughtException.WillBeOfType<UnregisteredDomainEventException>();
        }

        [TestMethod]
        public void Then_the_exception_will_have_the_following_message()
        {
            CaughtException?.Message.WillBe(string.Format("The requested domain event '{0}' is not registered in '{1}'", typeof(SomeUnregisteredEvent).FullName, typeof(TestAggregateRoot).FullName));
        }
    }
}