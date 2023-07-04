using Fohjin.DDD.Common;
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
    public class When_transfering_money_to_an_external_account : BaseTestFixture<MoneyTransferService>
    {
        protected override void SetupDependencies()
        {
            OnDependency<IReportingRepository>()
                ?.Setup(x => x.GetByExample<AccountReport>(It.IsAny<object>()))
                .Returns(new List<AccountReport> { new AccountReport(Guid.NewGuid(), Guid.NewGuid(), "AccountName", "target account number") });
        }

        protected override void Given()
        {
            // !!! This is DEMO code !!!
            // Setup the SystemRandom class to return the value where the account is not found

            Services
                .AddTransient<ISystemRandom>(_ => new TestSystemRandom((min, max) => 2))
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
            OnDependency<IReceiveMoneyTransfers>()?.Verify(x => x.Receive(It.IsAny<MoneyTransfer>()));
        }

        protected override void Finally()
        {
        }
    }
}