using Fohjin.DDD.Services;
using Fohjin.DDD.Services.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Test.Fohjin.DDD.TestUtilities.Tools
{
    public class TestSendMoneyTransfer : ISendMoneyTransfer
    {
        private readonly TestContext _testContext;
        private readonly IServiceProvider _serviceProvider;

        public TestSendMoneyTransfer(
            TestContext testContext,
            IServiceProvider serviceProvider
            )
        {
            _testContext = testContext;
            _serviceProvider = serviceProvider;
        }

        public void Send(MoneyTransfer moneyTransfer)
        {
            _testContext.AddResults("Send-MoneyTransfer", moneyTransfer);
        }
    }
}