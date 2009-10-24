using Fohjin.DDD.Configuration;
using Fohjin.DDD.Services;
using StructureMap;

namespace Fohjin.DDD.BankApplication
{
    public class ApplicationBootStrapper
    {
        public void BootStrapTheApplication()
        {
            DomainDatabaseBootStrapper.BootStrap();
            ReportingDatabaseBootStrapper.BootStrap();

            ObjectFactory.Initialize(x =>
            {
                x.AddRegistry<ApplicationRegistry>();
                x.AddRegistry<DomainRegistry>();
                x.AddRegistry<ReportingRegistry>();
                x.AddRegistry<CommandHandlerRegister>();
                x.AddRegistry<EventHandlerRegister>();
                x.AddRegistry<ServicesRegister>();
            });
            ObjectFactory.AssertConfigurationIsValid();
        }

        public static void BootStrap()
        {
            new ApplicationBootStrapper().BootStrapTheApplication();
        }
    }
}