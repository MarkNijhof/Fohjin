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
    public class When_changing_the_phone_number_of_a_client : CommandTestFixture<ChangeClientPhoneNumberCommand, ChangeClientPhoneNumberCommandHandler, Client>
    {
        protected override IEnumerable<IDomainEvent> Given()
        {
            yield return PrepareDomainEvent.Set(new ClientCreatedEvent(Guid.NewGuid(), "Mark Nijhof", "Welhavens gate", "49b", "5006", "Bergen", "95009937")).ToVersion(1);
        }

        protected override ChangeClientPhoneNumberCommand When()
        {
            return new ChangeClientPhoneNumberCommand(Guid.NewGuid(), "95009937");
        }

        [Then]
        public void Then_a_client_phone_number_changed_event_will_be_published()
        {
            PublishedEvents.Last().WillBeOfType<ClientPhoneNumberChangedEvent>();
        }

        [Then]
        public void Then_the_published_event_will_contain_the_new_phone_number_of_the_client()
        {
            PublishedEvents.Last<ClientPhoneNumberChangedEvent>().PhoneNumber.WillBe("95009937");
        }
    }

    public class When_changing_the_phone_number_of_a_not_yet_created_client : CommandTestFixture<ChangeClientPhoneNumberCommand, ChangeClientPhoneNumberCommandHandler, Client>
    {
        protected override IEnumerable<IDomainEvent> Given()
        {
            return new List<IDomainEvent>();
        }

        protected override ChangeClientPhoneNumberCommand When()
        {
            return new ChangeClientPhoneNumberCommand(Guid.NewGuid(), "95009937");
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

    public class When_the_phone_number_of_a_client_was_changed : EventTestFixture<ClientPhoneNumberChangedEvent, ClientPhoneNumberChangedEventHandler>
    {
        private static Guid _clientId;
        private object UpdateObject;
        private object WhereObject;

        protected override void MockSetup()
        {
            GetMock<IReportingRepository>()
                .Setup(x => x.Update<ClientDetailsReport>(It.IsAny<object>(), It.IsAny<object>()))
                .Callback<object, object>((u, w) => { UpdateObject = u; WhereObject = w; });
        }

        protected override ClientPhoneNumberChangedEvent When()
        {
            var clientPhoneNumberWasChangedEvent = new ClientPhoneNumberChangedEvent("1234567890") { AggregateId = Guid.NewGuid() };
            _clientId = clientPhoneNumberWasChangedEvent.AggregateId;
            return clientPhoneNumberWasChangedEvent;
        }

        [Then]
        public void Then_the_reporting_repository_will_be_used_to_update_the_client_details_report()
        {
            GetMock<IReportingRepository>().Verify(x => x.Update<ClientDetailsReport>(It.IsAny<object>(), It.IsAny<object>()));
        }

        [Then]
        public void Then_the_client_details_report_will_be_updated_with_the_expected_details()
        {
            UpdateObject.WillBeSimuliar(new { PhoneNumber = "1234567890" }.ToString());
            WhereObject.WillBeSimuliar(new { Id = _clientId });
        }
    }
}