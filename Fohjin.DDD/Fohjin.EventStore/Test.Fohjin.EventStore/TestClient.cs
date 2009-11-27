using System;
using System.Collections.Generic;
using Fohjin.EventStore;

namespace Test.Fohjin.EventStore
{
    public class TestClient
    {
        // Methods needed for plumbing
        protected virtual void Apply(object @event) { }
        protected IEnumerable<Type> RegisteredEvents()
        {
            yield return typeof(ClientMovedEvent);
        }

        // All internal state needs to be declared as protected proterties
        protected virtual Address Address { get; set; }

        // Domain behavior
        public void ClientMoves(Address address)
        {
            Apply(new ClientMovedEvent(address));
        }

        // Only needed to retrieve internal state for test
        public Address GetAddress()
        {
            return Address;
        }
    }

    public class ClientMovedEvent : IDomainEvent
    {
        public Guid EventId { get; private set; }
        public Guid AggregateId { get; set; }
        public int EventVersion { get; set; }

        public Address Address { get; private set; }

        public ClientMovedEvent(Address address)
        {
            EventId = Guid.NewGuid();
            Address = address;
        }
    }

    public class Address
    {
        public string Street { get; private set; }
        public string Number { get; private set; }
        public string PostalCode { get; private set; }
        public string City { get; private set; }

        public Address(string street, string nUmber, string postalCode, string city)
        {
            Street = street;
            Number = nUmber;
            PostalCode = postalCode;
            City = city;
        }
    }
}