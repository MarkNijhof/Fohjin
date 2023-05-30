using Fohjin.DDD.BankApplication;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Test.Fohjin.DDD.Configuration
{
    [TestClass]
    public class ApplicationBootStrapperTest
    {
        [TestMethod]
        public void Will_be_able_to_call_the_application_boot_strapper()
        {
            ApplicationBootStrapper.BootStrap();
        }
    }
}