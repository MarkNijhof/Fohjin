using Fohjin.DDD.CommandHandlers;
using Fohjin.DDD.Commands;
using Fohjin.DDD.Domain.Client;
using Fohjin.DDD.Events.Client;
using Fohjin.DDD.EventStore;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Test.Fohjin.DDD.Scenarios.Client_got_his_name_changed;

public class When_changing_the_name_of_a_client : CommandTestFixture<ChangeClientNameCommand, ChangeClientNameCommandHandler, Client>
{
    protected override IEnumerable<IDomainEvent> Given()
    {
        yield return PrepareDomainEvent.Set(new ClientCreatedEvent(Guid.NewGuid(), "Mark Nijhof", "Welhavens gate", "49b", "5006", "Bergen", "95009937")).ToVersion(1);
    }

    protected override ChangeClientNameCommand When() => new(Guid.NewGuid(), "Mark Nijhof");

    [TestMethod]
    public void Then_a_client_name_changed_event_will_be_published()
    {
        PublishedEvents?.Last().WillBeOfType<ClientNameChangedEvent>();
    }

    [TestMethod]
    public void Then_the_published_event_will_contain_the_new_name_of_the_client()
    {
        PublishedEvents?.Last<ClientNameChangedEvent>().ClientName.WillBe("Mark Nijhof");
    }
}