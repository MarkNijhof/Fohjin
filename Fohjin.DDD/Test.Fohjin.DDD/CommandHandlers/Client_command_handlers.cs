using System;
using System.Linq;
using System.Collections.Generic;
using Fohjin.DDD.CommandHandlers;
using Fohjin.DDD.Commands;
using Fohjin.DDD.Domain.Entities;
using Fohjin.DDD.Events;
using Fohjin.DDD.Events.Client;
using Test.Fohjin.DDD.Domain;

namespace Test.Fohjin.DDD.CommandHandlers
{
    public class When_providing_a_client_created_command : CommandHandlerTestFixture<ClientCreatedCommandHandler, Client>
    {
        protected override IEnumerable<IDomainEvent> Given()
        {
            return new List<IDomainEvent>();
        }

        protected override void When()
        {
            commandHandler.Execute(new ClientCreatedCommand(Guid.NewGuid(), "Mark Nijhof", "Welhavens gate", "49b", "5006", "Bergen", "95009937"));
        }

        [Then]
        public void Then_it_will_generate_a_new_client_created_event()
        {
            events.Last().WillBeOfType<NewClientCreatedEvent>();
        }

        [Then]
        public void Then_the_generated_new_client_created_event_will_contain_the_client_name()
        {
            events.Last<NewClientCreatedEvent>().ClientName.WillBe("Mark Nijhof");
        }

        [Then]
        public void Then_the_generated_new_client_created_event_will_contain_the_address()
        {
            events.Last<NewClientCreatedEvent>().Street.WillBe("Welhavens gate");
            events.Last<NewClientCreatedEvent>().StreetNumber.WillBe("49b");
            events.Last<NewClientCreatedEvent>().PostalCode.WillBe("5006");
            events.Last<NewClientCreatedEvent>().City.WillBe("Bergen");
        }

        [Then]
        public void Then_the_generated_new_client_created_event_will_contain_the_phonenumber()
        {
            events.Last<NewClientCreatedEvent>().PhoneNumber.WillBe("95009937");
        }
    }
}