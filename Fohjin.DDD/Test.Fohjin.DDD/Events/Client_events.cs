using System;
using Fohjin.DDD.EventHandlers;
using Fohjin.DDD.Events.Client;
using Fohjin.DDD.Reporting.Dto;
using Fohjin.DDD.Reporting.Infrastructure;
using Moq;
using NUnit.Framework;

namespace Test.Fohjin.DDD.Events
{
    public class Providing_an_new_client_created_event : EventTestFixture<NewClientCreatedEvent, NewClientCreatedEventHandler>
    {
        private static Guid _clientId;
        private Client _save_client;
        private ClientDetails _save_client_details;

        protected override void MockSetup()
        {
            GetMock<IReportingRepository>()
                .Setup(x => x.Save(It.IsAny<Client>()))
                .Callback<Client>(a => _save_client = a);

            GetMock<IReportingRepository>()
                .Setup(x => x.Save(It.IsAny<ClientDetails>()))
                .Callback<ClientDetails>(a => _save_client_details = a);
        }

        protected override NewClientCreatedEvent When()
        {
            _clientId = Guid.NewGuid();
            var clientCreatedEvent = new NewClientCreatedEvent(_clientId, "New Client Name", "Street", "123", "5000", "Bergen", "1234567890");
            return clientCreatedEvent;
        }

        [Then]
        public void Then_it_will_call_the_repository_is_called_to_save_the_account_dto()
        {
            GetMock<IReportingRepository>()
                .Verify(x => x.Save(It.IsAny<Client>()));
        }

        [Then]
        public void Then_it_will_call_the_repository_is_called_to_save_the_account_details_dto()
        {
            GetMock<IReportingRepository>()
                .Verify(x => x.Save(It.IsAny<ClientDetails>()));
        }

        [Then]
        public void Then_it_will_call_the_repository_with_the_correct_values_to_save_the_account()
        {
            _save_client.Id.WillBe(_clientId);
            _save_client.Name.WillBe("New Client Name");
        }

        [Then]
        public void Then_it_will_call_the_repository_with_the_correct_values_to_save_the_account_details()
        {
            _save_client_details.Id.WillBe(_clientId);
            _save_client_details.ClientName.WillBe("New Client Name");
            _save_client_details.Street.WillBe("Street");
            _save_client_details.StreetNumber.WillBe("123");
            _save_client_details.PostalCode.WillBe("5000");
            _save_client_details.City.WillBe("Bergen");
            _save_client_details.PhoneNumber.WillBe("1234567890");
        }
    }

    public class Providing_an_client_got_an_account_assigned_event : EventTestFixture<ClientGotAnAccountAssignedEvent, ClientGotAnAccountAssignedEventHandler>
    {
        protected override void MockSetup()
        {
        }

        protected override ClientGotAnAccountAssignedEvent When()
        {
            return new ClientGotAnAccountAssignedEvent(Guid.NewGuid()) { AggregateId = Guid.NewGuid() };
        }

        [Then]
        [ExpectedException(typeof(Exception))]
        public void Then_it_will_not_call_the_repository()
        {
            GetMock<IReportingRepository>();
        }

        [Then]
        public void Then_it_will_not_throw_an_exception()
        {
            caught.WillBeOfType<ThereWasNoExceptionButOneWasExpectedException>();
        }
    }

    public class Providing_an_client_has_moved_event : EventTestFixture<ClientHasMovedEvent, ClientHasMovedEventHandler>
    {
        private static Guid _clientId;
        private object _update_client_details;
        private object _where_client_details;

        protected override void MockSetup()
        {
            GetMock<IReportingRepository>()
                .Setup(x => x.Update<ClientDetails>(It.IsAny<object>(), It.IsAny<object>()))
                .Callback<object, object>((u, w) => { _update_client_details = u; _where_client_details = w; });
        }

        protected override ClientHasMovedEvent When()
        {
            var clientHasMovedEvent = new ClientHasMovedEvent("Street", "123", "5000", "Bergen") { AggregateId = Guid.NewGuid() };
            _clientId = clientHasMovedEvent.AggregateId;
            return clientHasMovedEvent;
        }

        [Then]
        public void Then_it_will_call_the_repository_is_called_to_update_the_account_details_dto()
        {
            GetMock<IReportingRepository>()
                .Verify(x => x.Update<ClientDetails>(It.IsAny<object>(), It.IsAny<object>()));
        }

        [Then]
        public void Then_it_will_call_the_repository_with_the_correct_values_to_update_the_account_details()
        {
            _update_client_details.WillBeSimuliar(new { Street = "Street", StreetNumber = "123", PostalCode = "5000", City = "Bergen" }.ToString());
        }

        [Then]
        public void Then_it_will_call_the_repository_with_the_correct_where_statement_for_the_account_details()
        {
            _where_client_details.WillBeSimuliar(new { Id = _clientId });
        }
    }

    public class Providing_an_client_name_was_changed_event : EventTestFixture<ClientNameWasChangedEvent, ClientNameWasChangedEventHandler>
    {
        private static Guid _clientId;
        private object _update_client;
        private object _where_client;
        private object _update_client_details;
        private object _where_client_details;

        protected override void MockSetup()
        {
            GetMock<IReportingRepository>()
                .Setup(x => x.Update<Client>(It.IsAny<object>(), It.IsAny<object>()))
                .Callback<object, object>((u, w) => { _update_client = u; _where_client = w; });

            GetMock<IReportingRepository>()
                .Setup(x => x.Update<ClientDetails>(It.IsAny<object>(), It.IsAny<object>()))
                .Callback<object, object>((u, w) => { _update_client_details = u; _where_client_details = w; });
        }

        protected override ClientNameWasChangedEvent When()
        {
            var clientNameWasChangedEvent = new ClientNameWasChangedEvent("New Client Name") { AggregateId = Guid.NewGuid() };
            _clientId = clientNameWasChangedEvent.AggregateId;
            return clientNameWasChangedEvent;
        }

        [Then]
        public void Then_it_will_call_the_repository_is_called_to_update_the_account_dto()
        {
            GetMock<IReportingRepository>()
                .Verify(x => x.Update<Client>(It.IsAny<object>(), It.IsAny<object>()));
        }

        [Then]
        public void Then_it_will_call_the_repository_is_called_to_update_the_account_details_dto()
        {
            GetMock<IReportingRepository>()
                .Verify(x => x.Update<ClientDetails>(It.IsAny<object>(), It.IsAny<object>()));
        }

        [Then]
        public void Then_it_will_call_the_repository_with_the_correct_values_to_update_the_account()
        {
            _update_client.WillBeSimuliar(new { Name = "New Client Name" }.ToString());
        }

        [Then]
        public void Then_it_will_call_the_repository_with_the_correct_values_to_update_the_account_details()
        {
            _update_client_details.WillBeSimuliar(new { ClientName = "New Client Name" }.ToString());
        }

        [Then]
        public void Then_it_will_call_the_repository_with_the_correct_where_statement_for_the_account()
        {
            _where_client.WillBeSimuliar(new { Id = _clientId });
        }

        [Then]
        public void Then_it_will_call_the_repository_with_the_correct_where_statement_for_the_account_details()
        {
            _where_client_details.WillBeSimuliar(new { Id = _clientId });
        }
    }

    public class Providing_an_client_phone_number_was_changed_event : EventTestFixture<ClientPhoneNumberWasChangedEvent, ClientPhoneNumberWasChangedEventHandler>
    {
        private static Guid _clientId;
        private object _update_client_details;
        private object _where_client_details;

        protected override void MockSetup()
        {
            GetMock<IReportingRepository>()
                .Setup(x => x.Update<ClientDetails>(It.IsAny<object>(), It.IsAny<object>()))
                .Callback<object, object>((u, w) => { _update_client_details = u; _where_client_details = w; });
        }

        protected override ClientPhoneNumberWasChangedEvent When()
        {
            var clientPhoneNumberWasChangedEvent = new ClientPhoneNumberWasChangedEvent("1234567890") { AggregateId = Guid.NewGuid() };
            _clientId = clientPhoneNumberWasChangedEvent.AggregateId;
            return clientPhoneNumberWasChangedEvent;
        }

        [Then]
        public void Then_it_will_call_the_repository_is_called_to_update_the_account_details_dto()
        {
            GetMock<IReportingRepository>()
                .Verify(x => x.Update<ClientDetails>(It.IsAny<object>(), It.IsAny<object>()));
        }

        [Then]
        public void Then_it_will_call_the_repository_with_the_correct_values_to_update_the_account_details()
        {
            _update_client_details.WillBeSimuliar(new { PhoneNumber = "1234567890" }.ToString());
        }

        [Then]
        public void Then_it_will_call_the_repository_with_the_correct_where_statement_for_the_account_details()
        {
            _where_client_details.WillBeSimuliar(new { Id = _clientId });
        }
    }
}