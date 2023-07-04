using Fohjin.DDD.Services;
using Fohjin.DDD.Services.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Test.Fohjin.DDD.TestUtilities.Tools
{
    public class TestSendMoneyTransfer : ISendMoneyTransfer
    {
        private readonly TestContext _testContext;

        public TestSendMoneyTransfer(
            TestContext testContext
            )
        {
            _testContext = testContext;
        }

        public void Send(MoneyTransfer moneyTransfer)
        {
            _testContext.AddResults("Send-MoneyTransfer", moneyTransfer);
        }
    }
}