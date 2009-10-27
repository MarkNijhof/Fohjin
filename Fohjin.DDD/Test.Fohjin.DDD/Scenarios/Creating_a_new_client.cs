using System;
using System.Linq;
using Fohjin.DDD.CommandHandlers;
using Fohjin.DDD.Commands;
using Fohjin.DDD.Contracts;
using Fohjin.DDD.Domain.Entities;
using Fohjin.DDD.EventHandlers;
using Fohjin.DDD.Events.Client;
using Fohjin.DDD.Reporting.Dto;
using Moq;

namespace Test.Fohjin.DDD.Scenarios
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

    public class When_a_new_client_was_created : EventTestFixture<ClientCreatedEvent, ClientCreatedEventHandler>
    {
        private static Guid _clientId;
        private ClientReport SaveClientObject;
        private ClientDetailsReport SaveClientDetailsObject;

        protected override void SetupDependencies()
        {
            OnDependency<IReportingRepository>()
                .Setup(x => x.Save(It.IsAny<ClientReport>()))
                .Callback<ClientReport>(a => SaveClientObject = a);

            OnDependency<IReportingRepository>()
                .Setup(x => x.Save(It.IsAny<ClientDetailsReport>()))
                .Callback<ClientDetailsReport>(a => SaveClientDetailsObject = a);
        }

        protected override ClientCreatedEvent When()
        {
            _clientId = Guid.NewGuid();
            var clientCreatedEvent = new ClientCreatedEvent(_clientId, "New Client Name", "Street", "123", "5000", "Bergen", "1234567890");
            return clientCreatedEvent;
        }

        [Then]
        public void Then_the_reporting_repository_will_be_used_to_save_the_client_report()
        {
            OnDependency<IReportingRepository>().Verify(x => x.Save(It.IsAny<ClientReport>()));
        }

        [Then]
        public void Then_the_client_report_will_be_updated_with_the_expected_details()
        {
            SaveClientObject.Id.WillBe(_clientId);
            SaveClientObject.Name.WillBe("New Client Name");
        }

        [Then]
        public void Then_the_reporting_repository_will_be_used_to_save_the_client_details_report()
        {
            OnDependency<IReportingRepository>().Verify(x => x.Save(It.IsAny<ClientDetailsReport>()));
        }

        [Then]
        public void Then_the_client_details_report_will_be_updated_with_the_expected_details()
        {
            SaveClientDetailsObject.Id.WillBe(_clientId);
            SaveClientDetailsObject.ClientName.WillBe("New Client Name");
            SaveClientDetailsObject.Street.WillBe("Street");
            SaveClientDetailsObject.StreetNumber.WillBe("123");
            SaveClientDetailsObject.PostalCode.WillBe("5000");
            SaveClientDetailsObject.City.WillBe("Bergen");
            SaveClientDetailsObject.PhoneNumber.WillBe("1234567890");
        }
    }
}