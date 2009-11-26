using System;
using System.Collections.Generic;
using Fohjin.EventStore;

namespace Test.Fohjin.EventStore
{
    public class TestObject
    {
        // Methods needed for plumbing
        protected virtual void Apply(object @event) { }
        protected IEnumerable<Type> RegisteredEvents()
        {
            yield return typeof(SomeEvent);
        }

        // All internal state needs to be declared as protected proterties
        protected virtual string Value { get; set; }

        // Domain behavior
        public void DoSomething(string value)
        {
            Apply(new SomeEvent(value));
        }

        // Only needed to retrieve internal state for test
        public string GetValue()
        {
            return Value;
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