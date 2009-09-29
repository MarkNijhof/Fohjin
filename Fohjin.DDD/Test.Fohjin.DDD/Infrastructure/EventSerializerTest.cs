using System;
using Fohjin.DDD.Domain.Events;
using Fohjin.DDD.Infrastructure;
using NUnit.Framework;
using NUnit.Framework.SyntaxHelpers;

namespace Test.Fohjin.DDD.Infrastructure
{
    [TestFixture]
    public class EventSerializerTest
    {
        private Serializer _serializer;

        [SetUp]
        public void SetUp()
        {
            _serializer = new Serializer();
        }

        [Test]
        public void The_event_serializer_will_be_able_to_correctly_serialize_and_deserialize_an_account_created_event()
        {
            var theEvent = new AccountCreatedEvent(Guid.NewGuid());

            var serializedEvent = _serializer.Serialize(theEvent);

            var domainEvent = _serializer.Deserialize<IDomainEvent>(serializedEvent) as AccountCreatedEvent;

            if (domainEvent == null)
                Assert.Fail("DomainEntity should not be null");

            Assert.That(domainEvent.Id, Is.EqualTo(theEvent.Id));
            Assert.That(domainEvent.TimeStamp, Is.EqualTo(theEvent.TimeStamp));
            Assert.That(domainEvent.Guid, Is.EqualTo(theEvent.Guid));
        }

        [Test]
        public void The_event_serializer_will_be_able_to_correctly_serialize_and_deserialize_an_account_closed_event()
        {
            var theEvent = new AccountClosedEvent();

            var serializedEvent = _serializer.Serialize(theEvent);

            var domainEvent = _serializer.Deserialize<IDomainEvent>(serializedEvent) as AccountClosedEvent;

            if (domainEvent == null)
                Assert.Fail("DomainEntity should not be null");

            Assert.That(domainEvent.Id, Is.EqualTo(theEvent.Id));
            Assert.That(domainEvent.TimeStamp, Is.EqualTo(theEvent.TimeStamp));
        }

        [Test]
        public void The_event_serializer_will_be_able_to_correctly_serialize_and_deserialize_a_withdrawl_event()
        {
            var theEvent = new WithdrawlEvent(10, 5);

            var serializedEvent = _serializer.Serialize(theEvent);

            var domainEvent = _serializer.Deserialize<IDomainEvent>(serializedEvent) as WithdrawlEvent;

            if (domainEvent == null)
                Assert.Fail("DomainEntity should not be null");

            Assert.That(domainEvent.Id, Is.EqualTo(theEvent.Id));
            Assert.That(domainEvent.TimeStamp, Is.EqualTo(theEvent.TimeStamp));
            Assert.That(domainEvent.Amount, Is.EqualTo(theEvent.Amount));
            Assert.That(domainEvent.Balance, Is.EqualTo(theEvent.Balance));
        }

        [Test]
        public void The_event_serializer_will_be_able_to_correctly_serialize_and_deserialize_a_deposite_event()
        {
            var theEvent = new DepositeEvent(10, 5);

            var serializedEvent = _serializer.Serialize(theEvent);

            var domainEvent = _serializer.Deserialize<IDomainEvent>(serializedEvent) as DepositeEvent;

            if (domainEvent == null)
                Assert.Fail("DomainEntity should not be null");

            Assert.That(domainEvent.Id, Is.EqualTo(theEvent.Id));
            Assert.That(domainEvent.TimeStamp, Is.EqualTo(theEvent.TimeStamp));
            Assert.That(domainEvent.Amount, Is.EqualTo(theEvent.Amount));
            Assert.That(domainEvent.Balance, Is.EqualTo(theEvent.Balance));
        }
    }
}