using Fohjin.DDD.Bus;
using Fohjin.DDD.Commands;
using Fohjin.DDD.Reporting;
using Fohjin.DDD.Reporting.Dtos;
using Fohjin.DDD.Services;
using Fohjin.DDD.Services.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace Test.Fohjin.DDD.Scenarios.Receiving_money_transfer
{
    public class When_receiving_a_money_transfer : BaseTestFixture<MoneyReceiveService>
    {
        protected override void SetupDependencies()
        {
            OnDependency<IReportingRepository>()
                ?.Setup(x => x.GetByExample<AccountReport>(It.IsAny<object>()))
                .Returns(new List<AccountReport> { new AccountReport(Guid.NewGuid(), Guid.NewGuid(), "AccountName", "target account number") });
        }

        protected override Task WhenAsync()
        {
            if (SubjectUnderTest == null)
                return Task.CompletedTask;

            SubjectUnderTest.Receive(new MoneyTransfer("source account number", "target account number", 123.45M));
            return Task.CompletedTask;
        }

        [TestMethod]
        public void Then_the_newly_created_account_will_be_saved()
        {
            OnDependency<IBus>()?.Verify(x => x.Publish(It.IsAny<ReceiveMoneyTransferCommand>()));
        }
    }
}