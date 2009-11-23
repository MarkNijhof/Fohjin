using Fohjin.DDD.Events;
using Fohjin.DDD.EventStore;
using Fohjin.DDD.EventStore.Aggregate;

namespace Test.Fohjin.DDD.Domain
{
    public class When_sending_an_internal_event_that_is_not_registered : AggregateRootTestFixture<TestAggregateRoot>
    {
        protected override void When()
        {
            AggregateRoot.DoSomethingIlligal();
        }

        [Then]
        public void Then_it_will_throw_an_unregistered_domain_event_exception()
        {
            CaughtException.WillBeOfType<UnregisteredDomainEventException>();
        }

        [Then]
        public void Then_the_exception_will_have_the_following_message()
        {
            CaughtException.Message.WillBe(string.Format("The requested domain event '{0}' is not registered in '{1}'", typeof(SomeUnregisteredEvent).FullName, typeof(TestAggregateRoot).FullName));
        }
    }

    public class When_triggering_behavior_on_the_aggregate_root_and_its_childeren_the_event_versions_will_match : AggregateRootTestFixture<TestAggregateRoot>
    {
        protected override void When()
        {
            AggregateRoot.DoSomething();
            AggregateRoot.Child.DoSomethingElse();
            AggregateRoot.DoSomething();
            AggregateRoot.Child.SomethingAbsolutelyElseWasDone();
        }

        [Then]
        public void Then_the_first_event_was_something_was_done()
        {
            PublishedEvents.LastMinus(3).WillBeOfType<SomethingWasDone>();
        }

        [Then]
        public void Then_the_first_event_will_have_version_number_1()
        {
            PublishedEvents.LastMinus<IDomainEvent>(3).Version.WillBe(1);
        }

        [Then]
        public void Then_the_second_event_was_something_was_done()
        {
            PublishedEvents.LastMinus(2).WillBeOfType<SomethingElseWasDone>();
        }

        [Then]
        public void Then_the_second_event_will_have_version_number_2()
        {
            PublishedEvents.LastMinus<IDomainEvent>(2).Version.WillBe(2);
        }

        [Then]
        public void Then_the_third_event_was_something_was_done()
        {
            PublishedEvents.LastMinus(1).WillBeOfType<SomethingWasDone>();
        }

        [Then]
        public void Then_the_third_event_will_have_version_number_3()
        {
            PublishedEvents.LastMinus<IDomainEvent>(1).Version.WillBe(3);
        }

        [Then]
        public void Then_the_fourth_event_was_something_was_done()
        {
            PublishedEvents.LastMinus(0).WillBeOfType<SomethingAbsolutelyElseWasDone>();
        }

        [Then]
        public void Then_the_fourth_event_will_have_version_number_4()
        {
            PublishedEvents.LastMinus<IDomainEvent>(0).Version.WillBe(4);
        }

    }

    public class TestAggregateRoot : BaseAggregateRoot<IDomainEvent>
    {
        private readonly EntityList<TestEntity, IDomainEvent> TestEntities;

        public TestAggregateRoot()
        {
            TestEntities = new EntityList<TestEntity, IDomainEvent>(this)
            {
                new TestEntity()
            };
            RegisterEvent<SomethingWasDone>(x => { });
        }

        public TestEntity Child { get { return TestEntities[0]; } }

        public void DoSomethingIlligal()
        {
            Apply(new SomeUnregisteredEvent());
        }

        public void DoSomething()
        {
            Apply(new SomethingWasDone());
        }
    }

    public class TestEntity : BaseEntity<IDomainEvent>
    {
        public TestEntity()
        {
            RegisterEvent<SomethingElseWasDone>(x => { });
            RegisterEvent<SomethingAbsolutelyElseWasDone>(x => { });
        }

        public void DoSomethingElse()
        {
            Apply(new SomethingElseWasDone());
        }

        public void SomethingAbsolutelyElseWasDone()
        {
            Apply(new SomethingAbsolutelyElseWasDone());
        }
    }

    public class SomeUnregisteredEvent : DomainEvent
    {
    }

    public class SomethingWasDone : DomainEvent
    {
    }

    public class SomethingElseWasDone : DomainEvent
    {
    }

    public class SomethingAbsolutelyElseWasDone : DomainEvent
    {
    }
}