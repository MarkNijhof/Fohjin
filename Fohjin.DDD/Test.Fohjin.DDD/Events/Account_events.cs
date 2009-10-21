using System;
using Fohjin.DDD.EventHandlers;
using Fohjin.DDD.Events.ActiveAccount;
using Fohjin.DDD.Events.Client;
using Fohjin.DDD.Reporting.Dto;
using Fohjin.DDD.Reporting.Infrastructure;
using Moq;
using NUnit.Framework;

namespace Test.Fohjin.DDD.Events
{
    public class Providing_an_account_closed_event : EventTestFixture<AccountClosedEvent, AccountClosedEventHandler>
    {
        private static Guid _accountId;
        private object _update_account;
        private object _where_account;
        private object _update_account_details;
        private object _where_account_details;

        protected override void MockSetup()
        {
            GetMock<IReportingRepository>()
                .Setup(x => x.Update<Account>(It.IsAny<object>(), It.IsAny<object>()))
                .Callback<object, object>((u, w) => { _update_account = u; _where_account = w; });

            GetMock<IReportingRepository>()
                .Setup(x => x.Update<AccountDetails>(It.IsAny<object>(), It.IsAny<object>()))
                .Callback<object, object>((u, w) => { _update_account_details = u; _where_account_details = w; });
        }

        protected override AccountClosedEvent When()
        {
            var accountClosedEvent = new AccountClosedEvent{ AggregateId = Guid.NewGuid() };
            _accountId = accountClosedEvent.AggregateId;
            return accountClosedEvent;
        }

        [Then]
        public void Then_it_will_call_the_repository_is_called_to_update_the_account_dto()
        {
            GetMock<IReportingRepository>()
                .Verify(x => x.Update<Account>(It.IsAny<object>(), It.IsAny<object>()));
        }

        [Then]
        public void Then_it_will_call_the_repository_is_called_to_update_the_account_details_dto()
        {
            GetMock<IReportingRepository>()
                .Verify(x => x.Update<AccountDetails>(It.IsAny<object>(), It.IsAny<object>()));
        }

        [Then]
        public void Then_it_will_call_the_repository_with_the_correct_values_to_update_the_account()
        {
            _update_account.WillBeSimuliar(new { Active = false }.ToString());
        }

        [Then]
        public void Then_it_will_call_the_repository_with_the_correct_values_to_update_the_account_details()
        {
            _update_account_details.WillBeSimuliar(new { Active = false }.ToString());
        }

        [Then]
        public void Then_it_will_call_the_repository_with_the_correct_where_statement_for_the_account()
        {
            _where_account.WillBeSimuliar(new { Id = _accountId });
        }

        [Then]
        public void Then_it_will_call_the_repository_with_the_correct_where_statement_for_the_account_details()
        {
            _where_account_details.WillBeSimuliar(new { Id = _accountId });
        }
    }

    public class Providing_an_account_created_event : EventTestFixture<AccountCreatedEvent, AccountCreatedEventHandler>
    {
        private static Guid _clientId;
        private static Guid _accountId;
        private Account _save_account;
        private AccountDetails _save_account_details;

        protected override void MockSetup()
        {
            GetMock<IReportingRepository>()
                .Setup(x => x.Save(It.IsAny<Account>()))
                .Callback<Account>(a => _save_account = a);

            GetMock<IReportingRepository>()
                .Setup(x => x.Save(It.IsAny<AccountDetails>()))
                .Callback<AccountDetails>(a => _save_account_details = a);
        }

        protected override AccountCreatedEvent When()
        {
            _accountId = Guid.NewGuid();
            _clientId = Guid.NewGuid();
            var accountCreatedEvent = new AccountCreatedEvent(_accountId, _clientId, "New Account", "1234567890");
            return accountCreatedEvent;
        }

        [Then]
        public void Then_it_will_call_the_repository_is_called_to_save_the_account_dto()
        {
            GetMock<IReportingRepository>()
                .Verify(x => x.Save(It.IsAny<Account>()));
        }

        [Then]
        public void Then_it_will_call_the_repository_is_called_to_save_the_account_details_dto()
        {
            GetMock<IReportingRepository>()
                .Verify(x => x.Save(It.IsAny<AccountDetails>()));
        }

        [Then]
        public void Then_it_will_call_the_repository_with_the_correct_values_to_save_the_account()
        {
            _save_account.Id.WillBe(_accountId);
            _save_account.ClientDetailsId.WillBe(_clientId);
            _save_account.Name.WillBe("New Account");
            _save_account.AccountNumber.WillBe("1234567890");
            _save_account.Active.WillBe(true);
        }

        [Then]
        public void Then_it_will_call_the_repository_with_the_correct_values_to_save_the_account_details()
        {
            _save_account_details.Id.WillBe(_accountId);
            _save_account_details.ClientId.WillBe(_clientId);
            _save_account_details.AccountName.WillBe("New Account");
            _save_account_details.AccountNumber.WillBe("1234567890");
            _save_account_details.Active.WillBe(true);
            _save_account_details.Balance.WillBe(0.0M);
        }
    }

    public class Providing_an_account_name_got_changed_event : EventTestFixture<AccountNameGotChangedEvent, AccountNameGotChangedEventHandler>
    {
        private static Guid _accountId;
        private object _update_account;
        private object _where_account;
        private object _update_account_details;
        private object _where_account_details;

        protected override void MockSetup()
        {
            GetMock<IReportingRepository>()
                .Setup(x => x.Update<Account>(It.IsAny<object>(), It.IsAny<object>()))
                .Callback<object, object>((u, w) => { _update_account = u; _where_account = w; });

            GetMock<IReportingRepository>()
                .Setup(x => x.Update<AccountDetails>(It.IsAny<object>(), It.IsAny<object>()))
                .Callback<object, object>((u, w) => { _update_account_details = u; _where_account_details = w; });
        }

        protected override AccountNameGotChangedEvent When()
        {
            var accountNameGotChangedEvent = new AccountNameGotChangedEvent("New Account Name") { AggregateId = Guid.NewGuid() };
            _accountId = accountNameGotChangedEvent.AggregateId;
            return accountNameGotChangedEvent;
        }

        [Then]
        public void Then_it_will_call_the_repository_is_called_to_update_the_account_dto()
        {
            GetMock<IReportingRepository>()
                .Verify(x => x.Update<Account>(It.IsAny<object>(), It.IsAny<object>()));
        }

        [Then]
        public void Then_it_will_call_the_repository_is_called_to_update_the_account_details_dto()
        {
            GetMock<IReportingRepository>()
                .Verify(x => x.Update<AccountDetails>(It.IsAny<object>(), It.IsAny<object>()));
        }

        [Then]
        public void Then_it_will_call_the_repository_with_the_correct_values_to_update_the_account()
        {
            _update_account.WillBeSimuliar(new { Name = "New Account Name" }.ToString());
        }

        [Then]
        public void Then_it_will_call_the_repository_with_the_correct_values_to_update_the_account_details()
        {
            _update_account_details.WillBeSimuliar(new { AccountName = "New Account Name" }.ToString());
        }

        [Then]
        public void Then_it_will_call_the_repository_with_the_correct_where_statement_for_the_account()
        {
            _where_account.WillBeSimuliar(new { Id = _accountId });
        }

        [Then]
        public void Then_it_will_call_the_repository_with_the_correct_where_statement_for_the_account_details()
        {
            _where_account_details.WillBeSimuliar(new { Id = _accountId });
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