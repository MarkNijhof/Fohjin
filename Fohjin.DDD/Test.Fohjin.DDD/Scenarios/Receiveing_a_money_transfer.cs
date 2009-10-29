using System;
using System.Collections.Generic;
using Fohjin.DDD.Bus;
using Fohjin.DDD.Commands;
using Fohjin.DDD.Contracts;
using Fohjin.DDD.Reporting.Dto;
using Fohjin.DDD.Services;
using Moq;

namespace Test.Fohjin.DDD.Scenarios
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
            OnDependency<ICommandBus>().Verify(x => x.Publish(It.IsAny<ReceiveMoneyTransferCommand>()));
        }
    }

    public class When_receiving_a_money_transfer_and_it_failed : BaseTestFixture<MoneyReceiveService>
    {
        protected override void SetupDependencies()
        {
            OnDependency<IReportingRepository>()
                .Setup(x => x.GetByExample<AccountReport>(It.IsAny<object>()))
                .Throws(new Exception("account not found"));
        }

        protected override void When()
        {
            SubjectUnderTest.Receive(new MoneyTransfer("source account number", "target account number", 123.45M));
        }

        [Then]
        public void Then_the_newly_created_account_will_be_saved()
        {
            CaughtException.WillBeOfType<AccountDoesNotExistException>();
        }

        [Then]
        public void Then_the_exception_message_will_be()
        {
            CaughtException.Message.WillBe(string.Format("The requested account '{0}' is not managed by this bank", "target account number"));
        }
    }
}