using Fohjin.DDD.Configuration;
using StructureMap;

namespace Fohjin.DDD.BankApplication
{
    public class ApplicationBootStrapper
    {
        public void BootStrapTheApplication()
        {
            new DomainDatabaseBootStrapper().ReCreateDatabaseSchema();
            new ReportingDatabaseBootStrapper().ReCreateDatabaseSchema();

            ObjectFactory.Initialize(x =>
            {
                x.AddRegistry<ApplicationRegistry>();
                x.AddRegistry<DomainRegistry>();
                x.AddRegistry<ReportingRegistry>();
            });
            ObjectFactory.AssertConfigurationIsValid();

            ExampleDataBootStrapper.BootStrap();
        }

        public static void BootStrap()
        {
            new ApplicationBootStrapper().BootStrapTheApplication();
        }
    }
}