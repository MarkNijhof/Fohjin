using System.IO;
using Fohjin.DDD.Configuration;
using NUnit.Framework;

namespace Test.Fohjin.DDD.Configuration
{
    [TestFixture]
    public class ReportingBootStrapperTest
    {
        [Test]
        public void Will_be_able_to_create_the_database_schema_in_sqlite_when_no_file_exists()
        {
            File.Delete(ReportingDatabaseBootStrapper.dataBaseFile);

            ReportingDatabaseBootStrapper.BootStrap();
        }

        [Test]
        public void Will_be_able_to_create_the_database_schema_in_sqlite()
        {
            new ReportingDatabaseBootStrapper().CreateDatabaseSchemaIfNeeded();
        }
        
        [Test]
        public void Will_be_able_to_re_create_the_database_schema_in_sqlite()
        {
            new ReportingDatabaseBootStrapper().ReCreateDatabaseSchema();
        }
    }
}