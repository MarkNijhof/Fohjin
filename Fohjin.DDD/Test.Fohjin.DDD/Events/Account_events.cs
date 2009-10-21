using System;
using Fohjin.DDD.EventHandlers;
using Fohjin.DDD.Events.ActiveAccount;
using Fohjin.DDD.Reporting.Dto;
using Fohjin.DDD.Reporting.Infrastructure;
using Moq;

namespace Test.Fohjin.DDD.Events
{
    public class Providing_an_account_closed_event : EventTestFixture<AccountClosedEvent, AccountClosedEventHandler>
    {
        private static Guid _guid;
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
            var accountClosedEvent = new AccountClosedEvent{ EntityId = Guid.NewGuid() };
            _guid = accountClosedEvent.EntityId;
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
            _where_account.WillBeSimuliar(new { Id = _guid });
        }

        [Then]
        public void Then_it_will_call_the_repository_with_the_correct_where_statement_for_the_account_details()
        {
            _where_account_details.WillBeSimuliar(new { Id = _guid });
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
}