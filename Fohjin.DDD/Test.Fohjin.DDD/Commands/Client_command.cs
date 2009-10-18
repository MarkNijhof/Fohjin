using System;
using System.Linq;
using System.Collections.Generic;
using Fohjin.DDD.CommandHandlers;
using Fohjin.DDD.Commands;
using Fohjin.DDD.Domain.Entities;
using Fohjin.DDD.Events;
using Fohjin.DDD.Events.Client;
using Test.Fohjin.DDD.Domain;

namespace Test.Fohjin.DDD.Commands
{
    public class When_providing_a_client_created_command : CommandTestFixture<ClientCreatedCommand, ClientCreatedCommandHandler, Client>
    {
        protected override IEnumerable<IDomainEvent> Given()
        {
            return new List<IDomainEvent>();
        }

        protected override ClientCreatedCommand When()
        {
            return new ClientCreatedCommand(Guid.NewGuid(), "Mark Nijhof", "Welhavens gate", "49b", "5006", "Bergen", "95009937");
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

    public class When_providing_a_client_phone_number_is_changed_command : CommandTestFixture<ClientPhoneNumberIsChangedCommand, ClientPhoneNumberIsChangedCommandHandler, Client>
    {
        protected override IEnumerable<IDomainEvent> Given()
        {
            return new List<IDomainEvent>();
        }

        protected override ClientPhoneNumberIsChangedCommand When()
        {
            return new ClientPhoneNumberIsChangedCommand(Guid.NewGuid(), "95009937");
        }

        [Then]
        public void Then_it_will_generate_a_new_client_created_event()
        {
            events.Last().WillBeOfType<ClientPhoneNumberWasChangedEvent>();
        }

        [Then]
        public void Then_the_generated_new_client_created_event_will_contain_the_phonenumber()
        {
            events.Last<ClientPhoneNumberWasChangedEvent>().PhoneNumber.WillBe("95009937");
        }
    }

    public class When_providing_a_client_changed_their_name_command : CommandTestFixture<ClientChangedTheirNameCommand, ClientChangedTheirNameCommandHandler, Client>
    {
        protected override IEnumerable<IDomainEvent> Given()
        {
            return new List<IDomainEvent>();
        }

        protected override ClientChangedTheirNameCommand When()
        {
            return new ClientChangedTheirNameCommand(Guid.NewGuid(), "Mark Nijhof");
        }

        [Then]
        public void Then_it_will_generate_a_new_client_created_event()
        {
            events.Last().WillBeOfType<ClientNameWasChangedEvent>();
        }

        [Then]
        public void Then_the_generated_new_client_created_event_will_contain_the_client_name()
        {
            events.Last<ClientNameWasChangedEvent>().ClientName.WillBe("Mark Nijhof");
        }
    }

    public class When_providing_a_client_has_moved_command : CommandTestFixture<ClientHasMovedCommand, ClientHasMovedCommandHandler, Client>
    {
        protected override IEnumerable<IDomainEvent> Given()
        {
            yield return DomainEvent.Set(new NewClientCreatedEvent(Guid.NewGuid(), "Mark Nijhof", "Welhavens gate", "49b", "5006", "Bergen", "95009937")).ToVersion(1);
        }

        protected override ClientHasMovedCommand When()
        {
            return new ClientHasMovedCommand(Guid.NewGuid(), "Welhavens gate", "49b", "5006", "Bergen");
        }

        [Then]
        public void Then_it_will_generate_a_client_has_moved_event()
        {
            events.Last().WillBeOfType<ClientHasMovedCommand>();
        }

        [Then]
        public void Then_the_generated_new_client_created_event_will_contain_the_address()
        {
            events.Last<ClientHasMovedCommand>().Street.WillBe("Welhavens gate");
            events.Last<ClientHasMovedCommand>().StreetNumber.WillBe("49b");
            events.Last<ClientHasMovedCommand>().PostalCode.WillBe("5006");
            events.Last<ClientHasMovedCommand>().City.WillBe("Bergen");
        }
    }
}