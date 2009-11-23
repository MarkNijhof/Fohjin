using Fohjin.DDD.BankApplication;
using NUnit.Framework;

namespace Test.Fohjin.DDD.Configuration
{
    [TestFixture]
    public class ApplicationBootStrapperTest
    {
        [Test]
        public void Will_be_able_to_call_the_application_boot_strapper()
        {
            ApplicationBootStrapper.BootStrap();
        }
    }
}