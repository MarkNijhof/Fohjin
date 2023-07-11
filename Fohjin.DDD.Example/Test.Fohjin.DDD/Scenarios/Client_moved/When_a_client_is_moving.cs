using Fohjin.DDD.CommandHandlers;
using Fohjin.DDD.Commands;
using Fohjin.DDD.Domain.Client;
using Fohjin.DDD.Events.Client;
using Fohjin.DDD.EventStore;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Test.Fohjin.DDD.Scenarios.Client_moved;

public class When_a_client_is_moving : CommandTestFixture<ClientIsMovingCommand, ClientIsMovingCommandHandler, Client>
{
    protected override IEnumerable<IDomainEvent> Given()
    {
        yield return PrepareDomainEvent.Set(new ClientCreatedEvent(Guid.NewGuid(), "Mark Nijhof", "Welhavens gate", "49b", "5006", "Bergen", "95009937")).ToVersion(1);
    }

    protected override ClientIsMovingCommand When() => new(Guid.NewGuid(), "Welhavens gate", "49b", "5006", "Bergen");

    [TestMethod]
    public void Then_a_client_Moved_changed_event_will_be_published()
    {
        PublishedEvents?.Last().WillBeOfType<ClientMovedEvent>();
    }

    [TestMethod]
    public void Then_the_published_event_will_contain_the_new_address_of_the_client()
    {
        PublishedEvents?.Last<ClientMovedEvent>().Street.WillBe("Welhavens gate");
        PublishedEvents?.Last<ClientMovedEvent>().StreetNumber.WillBe("49b");
        PublishedEvents?.Last<ClientMovedEvent>().PostalCode.WillBe("5006");
        PublishedEvents?.Last<ClientMovedEvent>().City.WillBe("Bergen");
    }
}