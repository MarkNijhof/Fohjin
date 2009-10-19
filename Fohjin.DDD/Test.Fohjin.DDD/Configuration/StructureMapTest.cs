using Fohjin.DDD.Configuration;
using NUnit.Framework;
using StructureMap;

namespace Test.Fohjin.DDD.Configuration
{
    [TestFixture]
    public class StructureMapTest
    {
        [Test]
        public void Will_be_able_to_re_create_the_database_schema_in_sqlite()
        {
            ObjectFactory.Initialize(x =>
            {
                x.AddRegistry<DomainRegistry>();
                x.AddRegistry<CommandHandlerRegister>();
                x.AddRegistry<EventHandlerRegister>();
                x.AddRegistry<ReportingRegistry>();
            });

            ObjectFactory.AssertConfigurationIsValid();

            ObjectFactory.ResetDefaults();
        }
    }
}