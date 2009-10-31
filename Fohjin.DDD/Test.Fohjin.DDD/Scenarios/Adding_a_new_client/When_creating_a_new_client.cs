using System;
using System.Linq;
using Fohjin.DDD.CommandHandlers;
using Fohjin.DDD.Commands;
using Fohjin.DDD.Domain.Client;
using Fohjin.DDD.Events.Client;

namespace Test.Fohjin.DDD.Scenarios.Adding_a_new_client
{
    public class When_creating_a_new_client : CommandTestFixture<CreateClientCommand, CreateClientCommandHandler, Client>
    {
        protected override CreateClientCommand When()
        {
            return new CreateClientCommand(Guid.NewGuid(), "Mark Nijhof", "Welhavens gate", "49b", "5006", "Bergen", "95009937");
        }

        [Then]
        public void Then_a_client_created_event_will_be_published()
        {
            PublishedEvents.Last().WillBeOfType<ClientCreatedEvent>();
        }

        [Then]
        public void Then_the_published_event_will_contain_the_name_of_the_client()
        {
            PublishedEvents.Last<ClientCreatedEvent>().ClientName.WillBe("Mark Nijhof");
        }

        [Then]
        public void Then_the_published_event_will_contain_the_address_of_the_client()
        {
            PublishedEvents.Last<ClientCreatedEvent>().Street.WillBe("Welhavens gate");
            PublishedEvents.Last<ClientCreatedEvent>().StreetNumber.WillBe("49b");
            PublishedEvents.Last<ClientCreatedEvent>().PostalCode.WillBe("5006");
            PublishedEvents.Last<ClientCreatedEvent>().City.WillBe("Bergen");
        }

        [Then]
        public void Then_the_published_event_will_contain_the_phone_number_of_the_client()
        {
            PublishedEvents.Last<ClientCreatedEvent>().PhoneNumber.WillBe("95009937");
        }
    }
}