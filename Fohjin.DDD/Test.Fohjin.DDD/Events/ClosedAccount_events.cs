using System;
using System.Collections.Generic;
using Fohjin.DDD.Contracts;
using Fohjin.DDD.EventHandlers;
using Fohjin.DDD.Events.ClosedAccount;
using Fohjin.DDD.Reporting.Dto;
using Moq;
using ClosedAccount=Fohjin.DDD.Reporting.Dto.ClosedAccount;
using Ledger=Fohjin.DDD.Reporting.Dto.Ledger;

namespace Test.Fohjin.DDD.Events
{
    public class Providing_an_new_closed_account_created_event : EventTestFixture<ClosedAccountCreatedEvent, ClosedAccountCreatedEventHandler>
    {
        private static Guid _orginalAccountId;
        private static Guid _clientId;
        private ClosedAccount _save_closed_account;
        private ClosedAccountDetails _save_closed_account_details;
        private List<KeyValuePair<string, string>> ledgers;
        private Guid _accountId;

        protected override void MockSetup()
        {
            GetMock<IReportingRepository>()
                .Setup(x => x.Save(It.IsAny<ClosedAccount>()))
                .Callback<ClosedAccount>(a => _save_closed_account = a);

            GetMock<IReportingRepository>()
                .Setup(x => x.Save(It.IsAny<ClosedAccountDetails>()))
                .Callback<ClosedAccountDetails>(a => _save_closed_account_details = a);
        }

        protected override ClosedAccountCreatedEvent When()
        {
            _accountId = Guid.NewGuid();
            _orginalAccountId = Guid.NewGuid();
            _clientId = Guid.NewGuid();

            ledgers = new List<KeyValuePair<string, string>>
            {
                new KeyValuePair<string, string>("CreditMutation" , "10.5|"),
                new KeyValuePair<string, string>("DebitMutation" , "15.0|"),
                new KeyValuePair<string, string>("CreditTransfer" , "10.5|1234567890"),
                new KeyValuePair<string, string>("DebitTransfer" , "15.0|0987654321"),
            };

            var closedAccountCreatedEvent = new ClosedAccountCreatedEvent(_accountId, _orginalAccountId, _clientId, ledgers, "Closed Account", "1234567890");
            return closedAccountCreatedEvent;
        }

        [Then]
        public void Then_it_will_call_the_repository_is_called_to_save_the_account_dto()
        {
            GetMock<IReportingRepository>()
                .Verify(x => x.Save(It.IsAny<ClosedAccount>()));
        }

        [Then]
        public void Then_it_will_call_the_repository_is_called_to_save_the_account_details_dto()
        {
            GetMock<IReportingRepository>()
                .Verify(x => x.Save(It.IsAny<ClosedAccountDetails>()));
        }

        [Then]
        public void Then_it_will_call_the_repository_is_called_to_save_the_4_ledgers_dto()
        {
            GetMock<IReportingRepository>()
                .Verify(x => x.Save(It.IsAny<Ledger>()), Times.Exactly(4));
        }

        [Then]
        public void Then_it_will_call_the_repository_with_the_correct_values_to_save_the_account()
        {
            _save_closed_account.Id.WillBe(_accountId);
            _save_closed_account.ClientDetailsId.WillBe(_clientId);
            _save_closed_account.Name.WillBe("Closed Account");
        }

        [Then]
        public void Then_it_will_call_the_repository_with_the_correct_values_to_save_the_account_details()
        {
            _save_closed_account_details.Id.WillBe(_accountId);
            _save_closed_account_details.ClientId.WillBe(_clientId);
            _save_closed_account_details.Balance.WillBe(0);
            _save_closed_account_details.AccountName.WillBe("Closed Account");
            _save_closed_account_details.AccountNumber.WillBe("1234567890");
        }
    }
}