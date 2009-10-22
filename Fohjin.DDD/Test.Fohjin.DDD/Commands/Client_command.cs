using System;
using System.Linq;
using System.Collections.Generic;
using Fohjin.DDD.CommandHandlers;
using Fohjin.DDD.Commands;
using Fohjin.DDD.Contracts;
using Fohjin.DDD.Domain.Entities;
using Fohjin.DDD.Domain.Exceptions;
using Fohjin.DDD.Events;
using Fohjin.DDD.Events.Client;
using Moq;

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

    public class When_providing_a_client_phone_number_is_changed_command_on_a_created_client : CommandTestFixture<ClientPhoneNumberIsChangedCommand, ClientPhoneNumberIsChangedCommandHandler, Client>
    {
        protected override IEnumerable<IDomainEvent> Given()
        {
            yield return PrepareDomainEvent.Set(new NewClientCreatedEvent(Guid.NewGuid(), "Mark Nijhof", "Welhavens gate", "49b", "5006", "Bergen", "95009937")).ToVersion(1);
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

    public class When_providing_a_client_phone_number_is_changed_command_on_a_not_created_client : CommandTestFixture<ClientPhoneNumberIsChangedCommand, ClientPhoneNumberIsChangedCommandHandler, Client>
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
        public void Then_it_will_throw_a_client_was_not_created_exception()
        {
            caught.WillBeOfType<ClientWasNotCreatedException>();
        }

        [Then]
        public void Then_the_throw_exception_message_will_be()
        {
            caught.Message.WillBe("The Client is not created and no opperations can be executed on it");
        }
    }

    public class When_providing_a_client_changed_their_name_command_on_a_created_client : CommandTestFixture<ClientChangedTheirNameCommand, ClientChangedTheirNameCommandHandler, Client>
    {
        protected override IEnumerable<IDomainEvent> Given()
        {
            yield return PrepareDomainEvent.Set(new NewClientCreatedEvent(Guid.NewGuid(), "Mark Nijhof", "Welhavens gate", "49b", "5006", "Bergen", "95009937")).ToVersion(1);
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

    public class When_providing_a_client_changed_their_name_command_on_a_not_created_client : CommandTestFixture<ClientChangedTheirNameCommand, ClientChangedTheirNameCommandHandler, Client>
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
        public void Then_it_will_throw_a_client_was_not_created_exception()
        {
            caught.WillBeOfType<ClientWasNotCreatedException>();
        }

        [Then]
        public void Then_the_throw_exception_message_will_be()
        {
            caught.Message.WillBe("The Client is not created and no opperations can be executed on it");
        }
    }

    public class When_providing_a_client_has_moved_command_on_a_created_client : CommandTestFixture<ClientHasMovedCommand, ClientHasMovedCommandHandler, Client>
    {
        protected override IEnumerable<IDomainEvent> Given()
        {
            yield return PrepareDomainEvent.Set(new NewClientCreatedEvent(Guid.NewGuid(), "Mark Nijhof", "Welhavens gate", "49b", "5006", "Bergen", "95009937")).ToVersion(1);
        }

        protected override ClientHasMovedCommand When()
        {
            return new ClientHasMovedCommand(Guid.NewGuid(), "Welhavens gate", "49b", "5006", "Bergen");
        }

        [Then]
        public void Then_it_will_generate_a_client_has_moved_event()
        {
            events.Last().WillBeOfType<ClientHasMovedEvent>();
        }

        [Then]
        public void Then_the_generated_new_client_created_event_will_contain_the_address()
        {
            events.Last<ClientHasMovedEvent>().Street.WillBe("Welhavens gate");
            events.Last<ClientHasMovedEvent>().StreetNumber.WillBe("49b");
            events.Last<ClientHasMovedEvent>().PostalCode.WillBe("5006");
            events.Last<ClientHasMovedEvent>().City.WillBe("Bergen");
        }
    }

    public class When_providing_a_client_has_moved_command_on_a_not_created_client : CommandTestFixture<ClientHasMovedCommand, ClientHasMovedCommandHandler, Client>
    {
        protected override IEnumerable<IDomainEvent> Given()
        {
            return new List<IDomainEvent>();
        }

        protected override ClientHasMovedCommand When()
        {
            return new ClientHasMovedCommand(Guid.NewGuid(), "Welhavens gate", "49b", "5006", "Bergen");
        }

        [Then]
        public void Then_it_will_throw_a_client_was_not_created_exception()
        {
            caught.WillBeOfType<ClientWasNotCreatedException>();
        }

        [Then]
        public void Then_the_throw_exception_message_will_be()
        {
            caught.Message.WillBe("The Client is not created and no opperations can be executed on it");
        }
    }

    public class When_providing_an_add_new_account_to_client_command_on_a_created_client : CommandTestFixture<AddNewAccountToClientCommand, AddNewAccountToClientCommandHandler, Client>
    {
        protected override IEnumerable<IDomainEvent> Given()
        {
            yield return PrepareDomainEvent.Set(new NewClientCreatedEvent(Guid.NewGuid(), "Mark Nijhof", "Welhavens gate", "49b", "5006", "Bergen", "95009937")).ToVersion(1);
        }

        protected override AddNewAccountToClientCommand When()
        {
            return new AddNewAccountToClientCommand(Guid.NewGuid(), "New Account");
        }

        [Then]
        public void Then_it_will_generate_a_client_has_moved_event()
        {
            events.Last().WillBeOfType<ClientGotAnAccountAssignedEvent>();
        }

        [Then]
        public void Then_the_generated_new_client_created_event_will_contain_the_id_of_the_client_it_will_be_assigned_too()
        {
            events.Last<ClientGotAnAccountAssignedEvent>().AggregateId.WillBe(aggregateRoot.Id);
        }

        [Then]
        public void Then_the_generated_new_client_created_event_will_contain_the_id_of_the_account()
        {
            events.Last<ClientGotAnAccountAssignedEvent>().AccountId.WillNotBe(new Guid());
        }

        [Then]
        public void Then_the_newly_created_account_will_be_saved()
        {
            GetMock<IDomainRepository>().Verify(x => x.Save(It.IsAny<ActiveAccount>()));
        }
    }

    public class When_providing_an_add_new_account_to_client_command_on_a_not_created_client : CommandTestFixture<AddNewAccountToClientCommand, AddNewAccountToClientCommandHandler, Client>
    {
        protected override IEnumerable<IDomainEvent> Given()
        {
            return new List<IDomainEvent>();
        }

        protected override AddNewAccountToClientCommand When()
        {
            return new AddNewAccountToClientCommand(Guid.NewGuid(), "New Account");
        }

        [Then]
        public void Then_it_will_throw_a_client_was_not_created_exception()
        {
            caught.WillBeOfType<ClientWasNotCreatedException>();
        }

        [Then]
        public void Then_the_throw_exception_message_will_be()
        {
            caught.Message.WillBe("The Client is not created and no opperations can be executed on it");
        }

        [Then]
        public void Then_the_newly_created_account_will_be_not_saved()
        {
            GetMock<IDomainRepository>().Verify(x => x.Save(It.IsAny<ActiveAccount>()), Times.Never());
        }
    }
}