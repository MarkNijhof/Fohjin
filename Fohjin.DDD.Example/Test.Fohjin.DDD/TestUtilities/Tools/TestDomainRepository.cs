using Fohjin.DDD.EventStore;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Test.Fohjin.DDD.TestUtilities.Tools
{
    public class TestDomainRepository<TDomainEvent> : IDomainRepository<TDomainEvent>
         where TDomainEvent : IDomainEvent
    {
        private readonly TestContext _testContext;
        private readonly IServiceProvider _serviceProvider;

        public TestDomainRepository(
            TestContext testContext,
            IServiceProvider serviceProvider
            )
        {
            _testContext = testContext;
            _serviceProvider = serviceProvider;
        }

        void IDomainRepository<TDomainEvent>.Add<TAggregate>(TAggregate aggregateRoot)
        {
            _testContext.AddResults(typeof(TAggregate).Name, aggregateRoot);
        }

        TAggregate IDomainRepository<TDomainEvent>.GetById<TAggregate>(Guid id)
        {
            var aggregate = (TAggregate)typeof(TAggregate).FillObject(_serviceProvider);
            aggregate.Id = id;
            return aggregate;
        }
    }
}