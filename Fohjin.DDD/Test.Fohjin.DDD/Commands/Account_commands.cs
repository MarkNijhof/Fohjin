using System.Linq;
using System.Collections.Generic;
using Fohjin.DDD.Domain.Entities;
using Fohjin.DDD.Events;
using Fohjin.DDD.Events.ActiveAccount;
using Test.Fohjin.DDD.Domain;

namespace Test.Fohjin.DDD.Commands
{
    public class When_calling_Create_New_on_ActiveAccount : AggregateRootTestFixture<ActiveAccount>
    {
        protected override IEnumerable<IDomainEvent> Given()
        {
            return new List<IDomainEvent>();
        }

        protected override void When()
        {
            aggregateRoot = ActiveAccount.CreateNew("New Account");
        }

        [Then]
        public void Then_it_will_generate_an_account_created_event()
        {
            events.Last().WillBeOfType<AccountCreatedEvent>();
        }

        [Then]
        public void Then_the_generated_new_account_created_event_will_contain_the_name_of_the_new_account()
        {
            events.Last<AccountCreatedEvent>().AccountName.WillBe("New Account");
        }
    }
}