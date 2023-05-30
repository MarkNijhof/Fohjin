using Fohjin.DDD.EventStore;
using Fohjin.DDD.EventStore.Aggregate;

namespace Test.Fohjin.DDD.Domain
{
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
}