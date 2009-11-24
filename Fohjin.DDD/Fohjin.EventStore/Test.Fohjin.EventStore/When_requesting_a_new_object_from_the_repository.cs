using System;
using Fohjin.EventStore;

namespace Test.Fohjin.EventStore
{
    public class When_requesting_a_new_object_from_the_repository : BaseTestFixture
    {
        private TestObject CreatedObject;

        protected override void When()
        {
            CreatedObject = new DomainRepository(new AggregateRootFactory()).CreateNew<TestObject>();
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

    public class When_executing_some_behavior_on_an_aggregate_root : BaseTestFixture
    {
        private TestObject CreatedObject;

        protected override void Given()
        {
            CreatedObject = new DomainRepository(new AggregateRootFactory()).CreateNew<TestObject>();
        }

        protected override void When()
        {
            CreatedObject.DoSomething("value");
        }

        [Then]
        public void Then_the_internal_event_will_be_applied()
        {
            CreatedObject.Value.WillBe("value");
        }
    }

    public class TestObject
    {
        private string _value;

        public string Value
        {
            get { return _value; }
        }

        public void DoSomething(string value)
        {
            Apply(new SomeEvent(value));
        }

        public virtual void Apply(object @event)
        {
            
        }

        private void onSomeEvent(IDomainEvent @event)
        {
            _value = ((SomeEvent)@event).Value;
        }
    }

    public class SomeEvent : IDomainEvent
    {
        public string Value { get; private set; }
        public Guid Id { get; private set; }
        public Guid AggregateId { get; set; }
        public int Version { get; set; }

        public SomeEvent(string value)
        {
            Value = value;
            Id = Guid.NewGuid();
        }
    }
}