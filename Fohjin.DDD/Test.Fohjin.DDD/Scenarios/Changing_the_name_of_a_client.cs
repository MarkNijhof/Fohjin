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
    public class When_changing_the_name_of_a_client : CommandTestFixture<ChangeClientNameCommand, ChangeClientNameCommandHandler, Client>
    {
        protected override IEnumerable<IDomainEvent> Given()
        {
            yield return PrepareDomainEvent.Set(new ClientCreatedEvent(Guid.NewGuid(), "Mark Nijhof", "Welhavens gate", "49b", "5006", "Bergen", "95009937")).ToVersion(1);
        }

        protected override ChangeClientNameCommand When()
        {
            return new ChangeClientNameCommand(Guid.NewGuid(), "Mark Nijhof");
        }

        [Then]
        public void Then_a_client_name_changed_event_will_be_published()
        {
            PublishedEvents.Last().WillBeOfType<ClientNameChangedEvent>();
        }

        [Then]
        public void Then_the_published_event_will_contain_the_new_name_of_the_client()
        {
            PublishedEvents.Last<ClientNameChangedEvent>().ClientName.WillBe("Mark Nijhof");
        }
    }

    public class When_changing_the_name_of_a_not_yet_created_client : CommandTestFixture<ChangeClientNameCommand, ChangeClientNameCommandHandler, Client>
    {
        protected override ChangeClientNameCommand When()
        {
            return new ChangeClientNameCommand(Guid.NewGuid(), "Mark Nijhof");
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

    public class When_the_name_of_a_client_was_changed : EventTestFixture<ClientNameChangedEvent, ClientNameChangedEventHandler>
    {
        private static Guid _clientId;
        private object UpdateClientObject;
        private object WhereClientObject;
        private object UpdateClientDetailsObject;
        private object WhereClientDetailsObject;

        protected override void SetupDependencies()
        {
            OnDependency<IReportingRepository>()
                .Setup(x => x.Update<ClientReport>(It.IsAny<object>(), It.IsAny<object>()))
                .Callback<object, object>((u, w) => { UpdateClientObject = u; WhereClientObject = w; });

            OnDependency<IReportingRepository>()
                .Setup(x => x.Update<ClientDetailsReport>(It.IsAny<object>(), It.IsAny<object>()))
                .Callback<object, object>((u, w) => { UpdateClientDetailsObject = u; WhereClientDetailsObject = w; });
        }

        protected override ClientNameChangedEvent When()
        {
            var clientNameWasChangedEvent = new ClientNameChangedEvent("New Client Name") { AggregateId = Guid.NewGuid() };
            _clientId = clientNameWasChangedEvent.AggregateId;
            return clientNameWasChangedEvent;
        }

        [Then]
        public void Then_the_reporting_repository_will_be_used_to_update_the_client_report()
        {
            OnDependency<IReportingRepository>().Verify(x => x.Update<ClientReport>(It.IsAny<object>(), It.IsAny<object>()));
        }

        [Then]
        public void Then_the_client_report_will_be_updated_with_the_expected_details()
        {
            UpdateClientObject.WillBeSimuliar(new { Name = "New Client Name" }.ToString());
            WhereClientObject.WillBeSimuliar(new { Id = _clientId });
        }

        [Then]
        public void Then_the_reporting_repository_will_be_used_to_update_the_client_details_report()
        {
            OnDependency<IReportingRepository>().Verify(x => x.Update<ClientDetailsReport>(It.IsAny<object>(), It.IsAny<object>()));
        }

        [Then]
        public void Then_the_client_details_report_will_be_updated_with_the_expected_details()
        {
            UpdateClientDetailsObject.WillBeSimuliar(new { ClientName = "New Client Name" }.ToString());
            WhereClientDetailsObject.WillBeSimuliar(new { Id = _clientId });
        }
    }
}