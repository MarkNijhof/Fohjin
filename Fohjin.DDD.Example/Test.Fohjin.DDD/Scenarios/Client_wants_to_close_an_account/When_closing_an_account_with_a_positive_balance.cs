using System;
using System.Collections.Generic;
using Fohjin.DDD.CommandHandlers;
using Fohjin.DDD.Commands;
using Fohjin.DDD.Domain.Account;
using Fohjin.DDD.Events.Account;
using Fohjin.DDD.EventStore;

namespace Test.Fohjin.DDD.Scenarios.Client_wants_to_close_an_account
{
    public class When_closing_an_account_with_a_positive_balance : CommandTestFixture<CloseAccountCommand, CloseAccountCommandHandler, ActiveAccount>
    {
        protected override IEnumerable<IDomainEvent> Given()
        {
            yield return PrepareDomainEvent.Set(new AccountOpenedEvent(Guid.NewGuid(), Guid.NewGuid(), "AccountName", "1234567890")).ToVersion(1);
            yield return PrepareDomainEvent.Set(new CashDepositedEvent(20, 20)).ToVersion(1);
        }

        protected override CloseAccountCommand When()
        {
            return new CloseAccountCommand(Guid.NewGuid());
        }

        [Then]
        public void Then_an_account_balance_not_zero_exception_will_be_thrown()
        {
            CaughtException.WillBeOfType<AccountBalanceNotZeroException>();
        }
    }
}