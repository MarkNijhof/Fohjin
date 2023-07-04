using System;
using System.Collections.Generic;
using Fohjin;
using Fohjin.DDD.Bus;
using Fohjin.DDD.Commands;
using Fohjin.DDD.Common;
using Fohjin.DDD.Domain;
using Fohjin.DDD.Reporting;
using Fohjin.DDD.Reporting.Dtos;
using Fohjin.DDD.Services;
using Fohjin.DDD.Services.Models;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Test.Fohjin.DDD.TestUtilities;

namespace Test.Fohjin.DDD.Scenarios.Transfering_money
{
    public class When_failing_to_transfer_money_to_an_internal_account : BaseTestFixture<MoneyTransferService>
    {
        protected override void SetupDependencies()
        {
            OnDependency<IBus>()
                ?.Setup(x => x.Publish(It.IsAny<ReceiveMoneyTransferCommand>()))
                .Throws(new Exception("exception message"));

            OnDependency<IReportingRepository>()
                ?.Setup(x => x.GetByExample<AccountReport>(It.IsAny<object>()))
                .Returns(new List<AccountReport> { new AccountReport(Guid.NewGuid(), Guid.NewGuid(), "AccountName", "target account number") });
        }

        protected override void Given()
        {
            // !!! This is DEMO code !!!
            // Setup the SystemRandom class to return the value where the account is not found
            Services
                .AddTransient<ISystemRandom>(_ => new TestSystemRandom((min, max) => 0))
                .AddTransient<ISystemTimer>(_ => new TestSystemTimer())
                ;
        }

        protected override Task WhenAsync()
        {
            SubjectUnderTest?.Send(new MoneyTransfer("source account number", "target account number", 123.45M));
            return Task.CompletedTask;
        }

        [TestMethod]
        public void Then_the_newly_created_account_will_be_saved()
        {
            OnDependency<IBus>()?.Verify(x => x.Publish(It.IsAny<MoneyTransferFailedCompensatingCommand>()));
        }

        protected override void Finally()
        {
        }
    }
}