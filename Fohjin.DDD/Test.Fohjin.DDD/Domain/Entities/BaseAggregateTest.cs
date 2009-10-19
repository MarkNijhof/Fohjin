using System;
using System.Collections.Generic;
using Fohjin.DDD.Domain;
using Fohjin.DDD.Domain.Exceptions;
using Fohjin.DDD.Events;
using NUnit.Framework;

namespace Test.Fohjin.DDD.Domain.Entities
{
    [TestFixture]
    public class When_sending_an_internal_event_that_is_not_registered : AggregateRootTestFixture<TestEntity>
    {
        protected override IEnumerable<IDomainEvent> Given()
        {
            return new List<IDomainEvent>();
        }

        protected override void When()
        {
            aggregateRoot.DoSomething();
        }

        [Then]
        public void Then_it_will_throw_an_exception()
        {
            caught.WillBeOfType<DomainEventWasNotRegisteredException>();
        }

        [Then]
        public void Then_the_exception_will_have_the_following_message()
        {
            caught.Message.WillBe(string.Format("The requested event '{0}' is not registered", typeof(SomeUnregisteredEvent).FullName));
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