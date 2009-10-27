using System;
using System.Linq;
using System.Collections.Generic;
using Fohjin.DDD.CommandHandlers;
using Fohjin.DDD.Commands;
using Fohjin.DDD.Contracts;
using Fohjin.DDD.Domain.Entities;
using Fohjin.DDD.Domain.Exceptions;
using Fohjin.DDD.EventHandlers;
using Fohjin.DDD.Events;
using Fohjin.DDD.Events.Client;
using Fohjin.DDD.Reporting.Dto;
using Moq;

namespace Test.Fohjin.DDD.Scenarios
{
    public class When_providing_a_client_has_moved_command_on_a_created_client : CommandTestFixture<MoveClientToNewAddressCommand, MoveClientToNewAddressCommandHandler, Client>
    {
        protected override IEnumerable<IDomainEvent> Given()
        {
            yield return PrepareDomainEvent.Set(new ClientCreatedEvent(Guid.NewGuid(), "Mark Nijhof", "Welhavens gate", "49b", "5006", "Bergen", "95009937")).ToVersion(1);
        }

        protected override MoveClientToNewAddressCommand When()
        {
            return new MoveClientToNewAddressCommand(Guid.NewGuid(), "Welhavens gate", "49b", "5006", "Bergen");
        }

        [Then]
        public void Then_a_client_Moved_changed_event_will_be_published()
        {
            PublishedEvents.Last().WillBeOfType<ClientMovedEvent>();
        }

        [Then]
        public void Then_the_published_event_will_contain_the_new_address_of_the_client()
        {
            PublishedEvents.Last<ClientMovedEvent>().Street.WillBe("Welhavens gate");
            PublishedEvents.Last<ClientMovedEvent>().StreetNumber.WillBe("49b");
            PublishedEvents.Last<ClientMovedEvent>().PostalCode.WillBe("5006");
            PublishedEvents.Last<ClientMovedEvent>().City.WillBe("Bergen");
        }
    }

    public class When_providing_a_client_has_moved_command_on_a_not_created_client : CommandTestFixture<MoveClientToNewAddressCommand, MoveClientToNewAddressCommandHandler, Client>
    {
        protected override MoveClientToNewAddressCommand When()
        {
            return new MoveClientToNewAddressCommand(Guid.NewGuid(), "Welhavens gate", "49b", "5006", "Bergen");
        }

        [Then]
        public void Then_a_client_was_not_created_exception_will_be_thrown()
        {
            CaughtException.WillBeOfType<ClientWasNotCreatedException>();
        }

        [Then]
        public void Then_the_exception_message_will_be()
        {
            CaughtException.Message.WillBe("The Client is not created and no opperations can be executed on it");
        }
    }

    public class Providing_an_client_has_moved_event : EventTestFixture<ClientMovedEvent, ClientMovedEventHandler>
    {
        private static Guid _clientId;
        private object UpdateClientDetailsObject;
        private object WhereClientDetailsObject;

        protected override void MockSetup()
        {
            GetMock<IReportingRepository>()
                .Setup(x => x.Update<ClientDetailsReport>(It.IsAny<object>(), It.IsAny<object>()))
                .Callback<object, object>((u, w) => { UpdateClientDetailsObject = u; WhereClientDetailsObject = w; });
        }

        protected override ClientMovedEvent When()
        {
            var clientHasMovedEvent = new ClientMovedEvent("Street", "123", "5000", "Bergen") { AggregateId = Guid.NewGuid() };
            _clientId = clientHasMovedEvent.AggregateId;
            return clientHasMovedEvent;
        }

        [Then]
        public void Then_the_reporting_repository_will_be_used_to_update_the_client_details_report()
        {
            GetMock<IReportingRepository>().Verify(x => x.Update<ClientDetailsReport>(It.IsAny<object>(), It.IsAny<object>()));
        }

        [Then]
        public void Then_the_client_details_report_will_be_updated_with_the_expected_details()
        {
            UpdateClientDetailsObject.WillBeSimuliar(new { Street = "Street", StreetNumber = "123", PostalCode = "5000", City = "Bergen" }.ToString());
            WhereClientDetailsObject.WillBeSimuliar(new { Id = _clientId });
        }
    }
}