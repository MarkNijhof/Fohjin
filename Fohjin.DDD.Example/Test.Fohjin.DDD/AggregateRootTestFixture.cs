using Fohjin.DDD.EventStore;

namespace Test.Fohjin.DDD;

//[TestClass]
public abstract class AggregateRootTestFixture<TAggregateRoot> where TAggregateRoot : IEventProvider<IDomainEvent>, new()
{
    protected TAggregateRoot AggregateRoot;
    protected Exception CaughtException;
    protected IEnumerable<IDomainEvent> PublishedEvents;
    protected virtual IEnumerable<IDomainEvent> Given()
    {
        return new List<IDomainEvent>();
    }
    protected virtual void Finally() {}
    protected abstract void When();

    //[Given]
    public void Setup()
    {
        CaughtException = new ThereWasNoExceptionButOneWasExpectedException();
        AggregateRoot = new TAggregateRoot();
        AggregateRoot.LoadFromHistory(Given());

        try
        {
            When();
            PublishedEvents = AggregateRoot.GetChanges();
        }
        catch (Exception exception)
        {
            CaughtException = exception;
        }
        finally
        {
            Finally();
        }
    }
}