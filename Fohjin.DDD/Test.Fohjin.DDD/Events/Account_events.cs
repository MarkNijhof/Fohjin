using System;
using Fohjin.DDD.EventHandlers;
using Fohjin.DDD.Events.ActiveAccount;
using Fohjin.DDD.Reporting.Dto;
using Fohjin.DDD.Reporting.Infrastructure;

namespace Test.Fohjin.DDD.Events
{
    public class Account_events : EventTestFixture<AccountClosedEvent, AccountClosedEventHandler>
    {
        private static Guid _guid;

        protected override AccountClosedEvent When()
        {
            var accountClosedEvent = new AccountClosedEvent();
            _guid = accountClosedEvent.Id;
            return accountClosedEvent;
        }

        [Then]
        public void Then_it_will_call_the_repository_with_the_following_update()
        {
            GetMock<IReportingRepository>().Verify(x => x.Update<Account>(new { Active = false }, new { Id = _guid }));
        }
    }
}