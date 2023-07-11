using Fohjin.DDD.CommandHandlers;
using Fohjin.DDD.Commands;
using Fohjin.DDD.Domain.Client;
using Fohjin.DDD.Events.Client;
using Fohjin.DDD.EventStore;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Test.Fohjin.DDD.Scenarios.Client_got_a_new_phone_number;

public class When_changing_the_phone_number : CommandTestFixture<ChangeClientPhoneNumberCommand, ChangeClientPhoneNumberCommandHandler, Client>
{
    protected override IEnumerable<IDomainEvent> Given()
    {
        yield return PrepareDomainEvent.Set(new ClientCreatedEvent(Guid.NewGuid(), "Mark Nijhof", "Welhavens gate", "49b", "5006", "Bergen", "95009937")).ToVersion(1);
    }

    protected override ChangeClientPhoneNumberCommand When() => new(Guid.NewGuid(), "95009937");

    [TestMethod]
    public void Then_a_client_phone_number_changed_event_will_be_published()
    {
        PublishedEvents?.Last().WillBeOfType<ClientPhoneNumberChangedEvent>();
    }

    [TestMethod]
    public void Then_the_published_event_will_contain_the_new_phone_number_of_the_client()
    {
        PublishedEvents?.Last<ClientPhoneNumberChangedEvent>().PhoneNumber.WillBe("95009937");
    }
}