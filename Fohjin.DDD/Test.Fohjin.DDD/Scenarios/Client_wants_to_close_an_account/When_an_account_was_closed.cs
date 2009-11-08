using System;
using Fohjin.DDD.EventHandlers;
using Fohjin.DDD.Events.Account;
using Fohjin.DDD.Reporting;
using Fohjin.DDD.Reporting.Dto;
using Moq;

namespace Test.Fohjin.DDD.Scenarios.Client_wants_to_close_an_account
{
    public class When_an_account_was_closed : EventTestFixture<AccountClosedEvent, AccountClosedEventHandler>
    {
        protected override AccountClosedEvent When()
        {
            return new AccountClosedEvent { AggregateId = Guid.NewGuid() };
        }

        [Then]
        public void Then_the_reporting_repository_will_be_used_to_update_the_account_report()
        {
            OnDependency<IReportingRepository>().Verify(x => x.Delete<AccountReport>(It.IsAny<object>()), Times.Once());
        }

        [Then]
        public void Then_the_reporting_repository_will_be_used_to_update_the_account_details_report()
        {
            OnDependency<IReportingRepository>().Verify(x => x.Delete<AccountDetailsReport>(It.IsAny<object>()), Times.Once());
        }
    }
}