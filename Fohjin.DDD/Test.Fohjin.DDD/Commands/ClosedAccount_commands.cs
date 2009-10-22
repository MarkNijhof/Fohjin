using System;
using System.Linq;
using System.Collections.Generic;
using Fohjin.DDD.Domain.Entities;
using Fohjin.DDD.Domain.ValueObjects;
using Fohjin.DDD.Events;
using Fohjin.DDD.Events.ClosedAccount;

namespace Test.Fohjin.DDD.Commands
{
    public class When_calling_Create_New_on_ClosedAccount : AggregateRootTestFixture<ClosedAccount>
    {
        private List<Ledger> ledgers;
        private Guid _accountId;
        private Guid _clientId;

        protected override IEnumerable<IDomainEvent> Given()
        {
            return new List<IDomainEvent>();
        }

        protected override void When()
        {
            ledgers = new List<Ledger>
            {
                new CreditMutation(10.5M, new AccountNumber(string.Empty)), 
                new DebitMutation(15.0M, new AccountNumber(string.Empty)),
                new CreditTransfer(10.5M, new AccountNumber("1234567890")), 
                new DebitTransfer(15.0M, new AccountNumber("0987654321")),
            };

            _accountId = Guid.NewGuid();
            _clientId = Guid.NewGuid();
            aggregateRoot = ClosedAccount.CreateNew(_accountId, _clientId, ledgers, new AccountName("Closed Account"), new AccountNumber("1234567890"));
        }

        [Then]
        public void Then_it_will_generate_an_closed_account_created_event()
        {
            events.Last().WillBeOfType<ClosedAccountCreatedEvent>();
        }

        [Then]
        public void Then_the_generated_closed_account_created_event_will_contain_the_details_of_the_closed_account()
        {
            events.Last<ClosedAccountCreatedEvent>().AccountId.WillBe(_accountId);
            events.Last<ClosedAccountCreatedEvent>().ClientId.WillBe(_clientId);
            events.Last<ClosedAccountCreatedEvent>().AccountName.WillBe("Closed Account");
            events.Last<ClosedAccountCreatedEvent>().AccountNumber.WillBe("1234567890");
        }

        [Then]
        public void Then_the_generated_new_account_created_event_will_contain_the_number_of_the_new_account()
        {
            events.Last<ClosedAccountCreatedEvent>().Ledgers.Count().WillBe(4);
            events.Last<ClosedAccountCreatedEvent>().Ledgers[0].Key.WillBe("CreditMutation");
            events.Last<ClosedAccountCreatedEvent>().Ledgers[1].Key.WillBe("DebitMutation");
            events.Last<ClosedAccountCreatedEvent>().Ledgers[2].Key.WillBe("CreditTransfer");
            events.Last<ClosedAccountCreatedEvent>().Ledgers[3].Key.WillBe("DebitTransfer");
        }
    }
}