using Fohjin.DDD.Reporting;
using Fohjin.DDD.Reporting.Dtos;
using Fohjin.DDD.Services;
using Fohjin.DDD.Services.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace Test.Fohjin.DDD.Scenarios.Receiving_money_transfer
{
    public class When_receiving_a_money_transfer_for_an_unknown_account : BaseTestFixture<MoneyReceiveService>
    {
        protected override void SetupDependencies()
        {
            OnDependency<IReportingRepository>()
                ?.Setup(x => x.GetByExample<AccountReport>(It.IsAny<object>()))
                .Throws(new Exception("account not found"));
        }

        protected override  Task WhenAsync()
        {
            SubjectUnderTest?.Receive(new MoneyTransfer("source account number", "target account number", 123.45M));
            return Task.CompletedTask;
        }

        [TestMethod]
        public void Then_the_newly_created_account_will_be_saved()
        {
            CaughtException.WillBeOfType<UnknownAccountException>();
        }

        [TestMethod]
        public void Then_the_exception_message_will_be()
        {
            CaughtException?.Message.WillBe(string.Format("The requested account '{0}' is not managed by this bank", "target account number"));
        }
    }
}