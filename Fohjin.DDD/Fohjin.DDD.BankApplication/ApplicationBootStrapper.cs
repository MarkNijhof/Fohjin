using Fohjin.DDD.Configuration;
using StructureMap;

namespace Fohjin.DDD.BankApplication
{
    public class ApplicationBootStrapper
    {
        public void BootStrapTheApplication()
        {
            new DatabaseBootStrapper().CreateDatabaseSchemaIfNeeded();

            ObjectFactory.Initialize(x =>
            {
                x.AddRegistry<ApplicationRegistry>();
                x.AddRegistry<DomainRegistry>();
            });
            ObjectFactory.AssertConfigurationIsValid();
        }

        public static void BootStrap()
        {
            new ApplicationBootStrapper().BootStrapTheApplication();
        }
    }
}