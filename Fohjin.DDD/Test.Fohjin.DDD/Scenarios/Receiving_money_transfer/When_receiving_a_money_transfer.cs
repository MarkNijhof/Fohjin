using System;
using System.Collections.Generic;
using Fohjin.DDD.Bus;
using Fohjin.DDD.Commands;
using Fohjin.DDD.Reporting;
using Fohjin.DDD.Reporting.Dto;
using Fohjin.DDD.Services;
using Moq;

namespace Test.Fohjin.DDD.Scenarios.Receiving_money_transfer
{
    public class When_receiving_a_money_transfer : BaseTestFixture<MoneyReceiveService>
    {
        protected override void SetupDependencies()
        {
            OnDependency<IReportingRepository>()
                .Setup(x => x.GetByExample<AccountReport>(It.IsAny<object>()))
                .Returns(new List<AccountReport> { new AccountReport(Guid.NewGuid(), Guid.NewGuid(), "AccountName", "target account number") });
        }

        protected override void When()
        {
            SubjectUnderTest.Receive(new MoneyTransfer("source account number", "target account number", 123.45M));
        }

        [Then]
        public void Then_the_newly_created_account_will_be_saved()
        {
            OnDependency<IBus>().Verify(x => x.Publish(It.IsAny<ReceiveMoneyTransferCommand>()));
        }
    }
}