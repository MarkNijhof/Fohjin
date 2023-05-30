using System.IO;
using Fohjin.DDD.Configuration;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Test.Fohjin.DDD.Configuration
{
    [TestClass]
    public class ReportingBootStrapperTest
    {
        [TestMethod]
        public void Will_be_able_to_create_the_database_schema_in_sqlite_when_no_file_exists()
        {
            File.Delete(ReportingDatabaseBootStrapper.dataBaseFile);

            ReportingDatabaseBootStrapper.BootStrap();
        }

        [TestMethod]
        public void Will_be_able_to_create_the_database_schema_in_sqlite()
        {
            new ReportingDatabaseBootStrapper().CreateDatabaseSchemaIfNeeded();
        }
        
        [TestMethod]
        public void Will_be_able_to_re_create_the_database_schema_in_sqlite()
        {
            new ReportingDatabaseBootStrapper().ReCreateDatabaseSchema();
        }
    }
}