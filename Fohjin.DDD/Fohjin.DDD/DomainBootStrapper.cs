using Fohjin.DDD.Configuration;
using StructureMap;

namespace Fohjin.DDD
{
    public class DomainBootStrapper
    {
        public void BootStrapTheDomain()
        {
            ObjectFactory.Initialize(x => x.AddRegistry(new DomainConfiguration()));
            ObjectFactory.AssertConfigurationIsValid();

            new DatabaseBootStrapper().CreateDatabaseSchemaIfNeeded();
        }

        public static void BootStrap()
        {
            new DomainBootStrapper().BootStrapTheDomain();
        }
    }
}