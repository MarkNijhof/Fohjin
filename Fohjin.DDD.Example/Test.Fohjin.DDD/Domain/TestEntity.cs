using Fohjin.DDD.EventStore;
using Fohjin.DDD.EventStore.Aggregate;

namespace Test.Fohjin.DDD.Domain
{
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
}