using System.IO;
using Fohjin.DDD.Configuration;
using NUnit.Framework;

namespace Test.Fohjin.DDD.Configuration
{
    [TestFixture]
    public class DatabaseBootStrapperTest
    {
        [Test]
        public void Will_be_able_to_create_the_database_schema_in_sqlite_when_no_file_exists()
        {
            File.Delete(DomainDatabaseBootStrapper.dataBaseFile);

            DomainDatabaseBootStrapper.BootStrap();
        }

        [Test]
        public void Will_be_able_to_create_the_database_schema_in_sqlite()
        {
            new DomainDatabaseBootStrapper().CreateDatabaseSchemaIfNeeded();
        }

        [Test]
        public void Will_be_able_to_re_create_the_database_schema_in_sqlite()
        {
            new DomainDatabaseBootStrapper().ReCreateDatabaseSchema();
        }
    }
}