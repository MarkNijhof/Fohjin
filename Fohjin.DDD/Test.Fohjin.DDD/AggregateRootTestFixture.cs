using System;
using System.Collections.Generic;
using Fohjin.DDD.Domain;
using Fohjin.DDD.Events;
using NUnit.Framework;

namespace Test.Fohjin.DDD
{
    [Specification]
    public abstract class AggregateRootTestFixture<TAggregateRoot> where TAggregateRoot : IEventProvider, new()
    {
        protected TAggregateRoot aggregateRoot;
        protected Exception caught;
        protected IEnumerable<IDomainEvent> events;

        protected abstract IEnumerable<IDomainEvent> Given();
        protected abstract void When();

        [SetUp]
        public void Setup()
        {
            caught = null;
            aggregateRoot = new TAggregateRoot();
            aggregateRoot.LoadFromHistory(Given());

            try
            {
                When();
                events = aggregateRoot.GetChanges();
            }
            catch (Exception e)
            {
                caught = e;
            }
        }
    }
}