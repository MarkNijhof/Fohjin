using Fohjin.DDD.Configuration;
using Fohjin.DDD.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using StructureMap;

namespace Test.Fohjin.DDD.Configuration
{
    [TestClass]
    public class StructureMapTest
    {
        [TestMethod]
        public void Will_be_able_to_re_create_the_database_schema_in_sqlite()
        {
            ObjectFactory.Initialize(x =>
            {
                x.AddRegistry<DomainRegistry>();
                x.AddRegistry<ReportingRegistry>();
                x.AddRegistry<ServicesRegister>();
            });

            ObjectFactory.AssertConfigurationIsValid();

            ObjectFactory.ResetDefaults();
        }
    }
}