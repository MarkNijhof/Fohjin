using Fohjin.DDD.EventStore;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Test.Fohjin.DDD.Domain;

public class When_triggering_behavior_on_the_aggregate_root_and_its_childeren_the_event_versions_will_match : AggregateRootTestFixture<TestAggregateRoot>
{
    protected override void When()
    {
        AggregateRoot?.DoSomething();
        AggregateRoot?.Child.DoSomethingElse();
        AggregateRoot?.DoSomething();
        AggregateRoot?.Child.SomethingAbsolutelyElseWasDone();
    }

    [TestMethod]
    public void Then_the_first_event_was_something_was_done()
    {
        PublishedEvents?.LastMinus(3).WillBeOfType<SomethingWasDone>();
    }

    [TestMethod]
    public void Then_the_first_event_will_have_version_number_1()
    {
        PublishedEvents.LastMinus<IDomainEvent>(3)?.Version.WillBe(1);
    }

    [TestMethod]
    public void Then_the_second_event_was_something_was_done()
    {
        PublishedEvents.LastMinus(2).WillBeOfType<SomethingElseWasDone>();
    }

    [TestMethod]
    public void Then_the_second_event_will_have_version_number_2()
    {
        PublishedEvents.LastMinus<IDomainEvent>(2)?.Version.WillBe(2);
    }

    [TestMethod]
    public void Then_the_third_event_was_something_was_done()
    {
        PublishedEvents.LastMinus(1).WillBeOfType<SomethingWasDone>();
    }

    [TestMethod]
    public void Then_the_third_event_will_have_version_number_3()
    {
        PublishedEvents.LastMinus<IDomainEvent>(1)?.Version.WillBe(3);
    }

    [TestMethod]
    public void Then_the_fourth_event_was_something_was_done()
    {
        PublishedEvents.LastMinus(0).WillBeOfType<SomethingAbsolutelyElseWasDone>();
    }

    [TestMethod]
    public void Then_the_fourth_event_will_have_version_number_4()
    {
        PublishedEvents.LastMinus<IDomainEvent>(0)?.Version.WillBe(4);
    }

}