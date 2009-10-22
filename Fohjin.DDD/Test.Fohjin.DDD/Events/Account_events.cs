using System;
using Fohjin.DDD.EventHandlers;
using Fohjin.DDD.Events.ActiveAccount;
using Fohjin.DDD.Reporting.Dto;
using Fohjin.DDD.Reporting.Infrastructure;
using Moq;

namespace Test.Fohjin.DDD.Events
{
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
        }

        [Then]
        public void Then_it_will_call_the_repository_with_the_correct_values_to_save_the_account_details()
        {
            _save_account_details.Id.WillBe(_accountId);
            _save_account_details.ClientId.WillBe(_clientId);
            _save_account_details.AccountName.WillBe("New Account");
            _save_account_details.AccountNumber.WillBe("1234567890");
            _save_account_details.Balance.WillBe(0.0M);
        }
    }

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
            var accountClosedEvent = new AccountClosedEvent { AggregateId = Guid.NewGuid() };
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

    public class Providing_an_deposite_event : EventTestFixture<DepositeEvent, DepositeEventHandler>
    {
        private static Guid _accountId;
        private object _update_account_details;
        private object _where_account_details;
        private Ledger _ledger;

        protected override void MockSetup()
        {
            GetMock<IReportingRepository>()
                .Setup(x => x.Update<AccountDetails>(It.IsAny<object>(), It.IsAny<object>()))
                .Callback<object, object>((u, w) => { _update_account_details = u; _where_account_details = w; });

            GetMock<IReportingRepository>()
                .Setup(x => x.Save(It.IsAny<Ledger>()))
                .Callback<Ledger>(l => { _ledger = l; });
        }

        protected override DepositeEvent When()
        {
            var depositeEvent = new DepositeEvent(50.5M, 10.5M) { AggregateId = Guid.NewGuid() };
            _accountId = depositeEvent.AggregateId;
            return depositeEvent;
        }

        [Then]
        public void Then_it_will_call_the_repository_is_called_to_update_the_account_details_dto()
        {
            GetMock<IReportingRepository>()
                .Verify(x => x.Update<AccountDetails>(It.IsAny<object>(), It.IsAny<object>()));
        }

        [Then]
        public void Then_it_will_call_the_repository_is_called_to_save_a_new_ledger_dto()
        {
            GetMock<IReportingRepository>()
                .Verify(x => x.Save(It.IsAny<Ledger>()));
        }

        [Then]
        public void Then_it_will_call_the_repository_with_the_correct_values_to_update_the_account_details()
        {
            _update_account_details.WillBeSimuliar(new { Balance = 50.5M }.ToString());
        }

        [Then]
        public void Then_it_will_call_the_repository_with_the_correct_values_to_save_the_ledger()
        {
            _ledger.AccountDetailsId.WillBe(_accountId);
            _ledger.Amount.WillBe(10.5M);
            _ledger.Action.WillBe("Deposite");
        }

        [Then]
        public void Then_it_will_call_the_repository_with_the_correct_where_statement_for_the_account_details()
        {
            _where_account_details.WillBeSimuliar(new { Id = _accountId });
        }
    }

    public class Providing_an_withdrawl_event : EventTestFixture<WithdrawlEvent, WithdrawlEventHandler>
    {
        private static Guid _accountId;
        private object _update_account_details;
        private object _where_account_details;
        private Ledger _ledger;

        protected override void MockSetup()
        {
            GetMock<IReportingRepository>()
                .Setup(x => x.Update<AccountDetails>(It.IsAny<object>(), It.IsAny<object>()))
                .Callback<object, object>((u, w) => { _update_account_details = u; _where_account_details = w; });

            GetMock<IReportingRepository>()
                .Setup(x => x.Save(It.IsAny<Ledger>()))
                .Callback<Ledger>(l => { _ledger = l; });
        }

        protected override WithdrawlEvent When()
        {
            var withdrawlEvent = new WithdrawlEvent(50.5M, 10.5M) { AggregateId = Guid.NewGuid() };
            _accountId = withdrawlEvent.AggregateId;
            return withdrawlEvent;
        }

        [Then]
        public void Then_it_will_call_the_repository_is_called_to_update_the_account_details_dto()
        {
            GetMock<IReportingRepository>()
                .Verify(x => x.Update<AccountDetails>(It.IsAny<object>(), It.IsAny<object>()));
        }

        [Then]
        public void Then_it_will_call_the_repository_is_called_to_save_a_new_ledger_dto()
        {
            GetMock<IReportingRepository>()
                .Verify(x => x.Save(It.IsAny<Ledger>()));
        }

        [Then]
        public void Then_it_will_call_the_repository_with_the_correct_values_to_update_the_account_details()
        {
            _update_account_details.WillBeSimuliar(new { Balance = 50.5M }.ToString());
        }

        [Then]
        public void Then_it_will_call_the_repository_with_the_correct_values_to_save_the_ledger()
        {
            _ledger.AccountDetailsId.WillBe(_accountId);
            _ledger.Amount.WillBe(10.5M);
            _ledger.Action.WillBe("Withdrawl");
        }

        [Then]
        public void Then_it_will_call_the_repository_with_the_correct_where_statement_for_the_account_details()
        {
            _where_account_details.WillBeSimuliar(new { Id = _accountId });
        }
    }

    public class Providing_an_money_transfer_from_an_other_account_event : EventTestFixture<MoneyTransferedFromAnOtherAccountEvent, MoneyTransferedFromAnOtherAccountEventHandler>
    {
        private static Guid _accountId;
        private object _update_account_details;
        private object _where_account_details;
        private Ledger _ledger;

        protected override void MockSetup()
        {
            GetMock<IReportingRepository>()
                .Setup(x => x.Update<AccountDetails>(It.IsAny<object>(), It.IsAny<object>()))
                .Callback<object, object>((u, w) => { _update_account_details = u; _where_account_details = w; });

            GetMock<IReportingRepository>()
                .Setup(x => x.Save(It.IsAny<Ledger>()))
                .Callback<Ledger>(l => { _ledger = l; });
        }

        protected override MoneyTransferedFromAnOtherAccountEvent When()
        {
            var moneyTransferedFromAnOtherAccountEvent = new MoneyTransferedFromAnOtherAccountEvent(50.5M, 10.5M, "1234567890") { AggregateId = Guid.NewGuid() };
            _accountId = moneyTransferedFromAnOtherAccountEvent.AggregateId;
            return moneyTransferedFromAnOtherAccountEvent;
        }

        [Then]
        public void Then_it_will_call_the_repository_is_called_to_update_the_account_details_dto()
        {
            GetMock<IReportingRepository>()
                .Verify(x => x.Update<AccountDetails>(It.IsAny<object>(), It.IsAny<object>()));
        }

        [Then]
        public void Then_it_will_call_the_repository_is_called_to_save_a_new_ledger_dto()
        {
            GetMock<IReportingRepository>()
                .Verify(x => x.Save(It.IsAny<Ledger>()));
        }

        [Then]
        public void Then_it_will_call_the_repository_with_the_correct_values_to_update_the_account_details()
        {
            _update_account_details.WillBeSimuliar(new { Balance = 50.5M }.ToString());
        }

        [Then]
        public void Then_it_will_call_the_repository_with_the_correct_values_to_save_the_ledger()
        {
            _ledger.AccountDetailsId.WillBe(_accountId);
            _ledger.Amount.WillBe(10.5M);
            _ledger.Action.WillBe("Transfer from 1234567890");
        }

        [Then]
        public void Then_it_will_call_the_repository_with_the_correct_where_statement_for_the_account_details()
        {
            _where_account_details.WillBeSimuliar(new { Id = _accountId });
        }
    }

    public class Providing_an_money_transfer_to_an_other_account_event : EventTestFixture<MoneyTransferedToAnOtherAccountEvent, MoneyTransferedToAnOtherAccountEventHandler>
    {
        private static Guid _accountId;
        private object _update_account_details;
        private object _where_account_details;
        private Ledger _ledger;

        protected override void MockSetup()
        {
            GetMock<IReportingRepository>()
                .Setup(x => x.Update<AccountDetails>(It.IsAny<object>(), It.IsAny<object>()))
                .Callback<object, object>((u, w) => { _update_account_details = u; _where_account_details = w; });

            GetMock<IReportingRepository>()
                .Setup(x => x.Save(It.IsAny<Ledger>()))
                .Callback<Ledger>(l => { _ledger = l; });
        }

        protected override MoneyTransferedToAnOtherAccountEvent When()
        {
            var moneyTransferedToAnOtherAccountEvent = new MoneyTransferedToAnOtherAccountEvent(50.5M, 10.5M, "1234567890") { AggregateId = Guid.NewGuid() };
            _accountId = moneyTransferedToAnOtherAccountEvent.AggregateId;
            return moneyTransferedToAnOtherAccountEvent;
        }

        [Then]
        public void Then_it_will_call_the_repository_is_called_to_update_the_account_details_dto()
        {
            GetMock<IReportingRepository>()
                .Verify(x => x.Update<AccountDetails>(It.IsAny<object>(), It.IsAny<object>()));
        }

        [Then]
        public void Then_it_will_call_the_repository_is_called_to_save_a_new_ledger_dto()
        {
            GetMock<IReportingRepository>()
                .Verify(x => x.Save(It.IsAny<Ledger>()));
        }

        [Then]
        public void Then_it_will_call_the_repository_with_the_correct_values_to_update_the_account_details()
        {
            _update_account_details.WillBeSimuliar(new { Balance = 50.5M }.ToString());
        }

        [Then]
        public void Then_it_will_call_the_repository_with_the_correct_values_to_save_the_ledger()
        {
            _ledger.AccountDetailsId.WillBe(_accountId);
            _ledger.Amount.WillBe(10.5M);
            _ledger.Action.WillBe("Transfer to 1234567890");
        }

        [Then]
        public void Then_it_will_call_the_repository_with_the_correct_where_statement_for_the_account_details()
        {
            _where_account_details.WillBeSimuliar(new { Id = _accountId });
        }
    }
}