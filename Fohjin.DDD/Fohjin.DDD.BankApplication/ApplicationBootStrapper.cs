using Fohjin.DDD.Configuration;
using StructureMap;

namespace Fohjin.DDD.BankApplication
{
    public class ApplicationBootStrapper
    {
        public void BootStrapTheApplication()
        {
            DomainDatabaseBootStrapper.BootStrap();
            ReportingDatabaseBootStrapper.BootStrap();
            //new DomainDatabaseBootStrapper().ReCreateDatabaseSchema();
            //new ReportingDatabaseBootStrapper().ReCreateDatabaseSchema();

            ObjectFactory.Initialize(x =>
            {
                x.AddRegistry<ApplicationRegistry>();
                x.AddRegistry<DomainRegistry>();
                x.AddRegistry<ReportingRegistry>();
                x.AddRegistry<CommandHandlerRegister>();
                x.AddRegistry<EventHandlerRegister>();
            });
            ObjectFactory.AssertConfigurationIsValid();

            //ExampleDataBootStrapper.BootStrap();
        }

        public static void BootStrap()
        {
            new ApplicationBootStrapper().BootStrapTheApplication();
        }
    }
}