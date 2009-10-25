using System;
using System.Collections.Generic;
using Fohjin.DDD.Bus;
using Fohjin.DDD.Commands;
using Fohjin.DDD.Contracts;
using Fohjin.DDD.Domain;
using Fohjin.DDD.Reporting.Dto;
using Fohjin.DDD.Services;
using Moq;

namespace Test.Fohjin.DDD.Services
{
    public class When_sending_a_money_transfer_internal_account : BaseTestFixture<MoneyTransferService>
    {
        protected override void MockSetup()
        {
            GetMock<IReportingRepository>()
                .Setup(x => x.GetByExample<AccountReport>(It.IsAny<object>()))
                .Returns(new List<AccountReport> { new AccountReport(Guid.NewGuid(), Guid.NewGuid(), "AccountName", "target account number") });
        }

        protected override void Given()
        {
            // !!! This is DEMO code !!!
            // Setup the SystemRandom class to return the value where the account is found
            SystemRandom.Next = (min, max) => 0;
        }

        protected override void When()
        {
            SubjectUnderTest.Send(new MoneyTransfer("source account number", "target account number", 123.45M));
        }

        [Then]
        public void Then_the_newly_created_account_will_be_saved()
        {
            GetMock<ICommandBus>().Verify(x => x.Publish(It.IsAny<ReceiveMoneyTransferCommand>()));

            SystemRandom.Reset();
        }
    }

    public class When_sending_a_money_transfer_internal_account_and_it_failed : BaseTestFixture<MoneyTransferService>
    {
        protected override void MockSetup()
        {
            GetMock<ICommandBus>()
                .Setup(x => x.Publish(It.IsAny<ReceiveMoneyTransferCommand>()))
                .Throws(new Exception("exception message"));

            GetMock<IReportingRepository>()
                .Setup(x => x.GetByExample<AccountReport>(It.IsAny<object>()))
                .Returns(new List<AccountReport> { new AccountReport(Guid.NewGuid(), Guid.NewGuid(), "AccountName", "target account number") });
        }

        protected override void Given()
        {
            // !!! This is DEMO code !!!
            // Setup the SystemRandom class to return the value where the account is not found
            SystemRandom.Next = (min, max) => 0;
        }

        protected override void When()
        {
            SubjectUnderTest.Send(new MoneyTransfer("source account number", "target account number", 123.45M));
        }

        [Then]
        public void Then_the_newly_created_account_will_be_saved()
        {
            GetMock<ICommandBus>().Verify(x => x.Publish(It.IsAny<MoneyTransferFailedCommand>()));

            SystemRandom.Reset();
        }
    }

    public class When_sending_a_money_transfer_external_account : BaseTestFixture<MoneyTransferService>
    {
        protected override void MockSetup()
        {
            GetMock<IReportingRepository>()
                .Setup(x => x.GetByExample<AccountReport>(It.IsAny<object>()))
                .Returns(new List<AccountReport> { new AccountReport(Guid.NewGuid(), Guid.NewGuid(), "AccountName", "target account number") });
        }

        protected override void Given()
        {
            // !!! This is DEMO code !!!
            // Setup the SystemRandom class to return the value where the account is not found
            SystemRandom.Next = (min, max) => 1;
        }

        protected override void When()
        {
            SubjectUnderTest.Send(new MoneyTransfer("source account number", "target account number", 123.45M));
        }

        [Then]
        public void Then_the_newly_created_account_will_be_saved()
        {
            GetMock<IReceiveMoneyTransfers>().Verify(x => x.Receive(It.IsAny<MoneyTransfer>()));

            SystemRandom.Reset();
        }
    }

    public class When_sending_a_money_transfer_external_account_and_it_failed : BaseTestFixture<MoneyTransferService>
    {
        protected override void MockSetup()
        {
            GetMock<IReceiveMoneyTransfers>()
                .Setup(x => x.Receive(It.IsAny<MoneyTransfer>()))
                .Throws(new AccountDoesNotExistException("exception message"));

            GetMock<IReportingRepository>()
                .Setup(x => x.GetByExample<AccountReport>(It.IsAny<object>()))
                .Returns(new List<AccountReport> { new AccountReport(Guid.NewGuid(), Guid.NewGuid(), "AccountName", "target account number") });
        }

        protected override void Given()
        {
            // !!! This is DEMO code !!!
            // Setup the SystemRandom class to return the value where the account is not found
            SystemRandom.Next = (min, max) => 2;
        }

        protected override void When()
        {
            SubjectUnderTest.Send(new MoneyTransfer("source account number", "target account number", 123.45M));
        }

        [Then]
        public void Then_the_newly_created_account_will_be_saved()
        {
            GetMock<ICommandBus>().Verify(x => x.Publish(It.IsAny<MoneyTransferFailedCommand>()));

            SystemRandom.Reset();
        }
    }
}