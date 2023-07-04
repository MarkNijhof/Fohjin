using Fohjin.DDD.CommandHandlers;
using Fohjin.DDD.Commands;
using Fohjin.DDD.Domain.Account;
using Fohjin.DDD.Domain.Client;
using Fohjin.DDD.Events.Client;
using Fohjin.DDD.EventStore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace Test.Fohjin.DDD.Scenarios.Client_wants_to_open_a_new_account;

public class When_opening_a_new_account : CommandTestFixture<OpenNewAccountForClientCommand, OpenNewAccountForClientCommandHandler, Client>
{
    protected override IEnumerable<IDomainEvent> Given()
    {
        yield return PrepareDomainEvent.Set(new ClientCreatedEvent(Guid.NewGuid(), "Mark Nijhof", "Welhavens gate", "49b", "5006", "Bergen", "95009937")).ToVersion(1);
    }

    protected override OpenNewAccountForClientCommand When() => new(Guid.NewGuid(), "New Account");

    [TestMethod]
    public void Then_an_account_to_client_assigned_event_will_be_published()
    {
        PublishedEvents?.Last().WillBeOfType<AccountToClientAssignedEvent>();
    }

    [TestMethod]
    public void Then_the_published_event_will_contain_the_expected_details_of_the_account()
    {
        PublishedEvents?.Last<AccountToClientAssignedEvent>().AggregateId.WillBe(AggregateRoot?.Id);
        PublishedEvents?.Last<AccountToClientAssignedEvent>().AccountId.WillNotBe(Guid.Empty);
    }

    [TestMethod]
    public void Then_the_newly_created_account_will_be_saved()
    {
        OnDependency<IDomainRepository<IDomainEvent>>().Verify(x => x.Add(It.IsAny<ActiveAccount>()));
    }
}