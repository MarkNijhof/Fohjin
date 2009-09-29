using Fohjin.DDD.Configuration;
using NUnit.Framework;

namespace Test.Fohjin.DDD.Configuration
{
    [TestFixture]
    public class DatabaseBootStrapperTest
    {
        [Test]
        public void Will_be_able_to_create_the_database_schema_in_sqlite()
        {
            new DatabaseBootStrapper().ReCreateDatabaseSchema();
        }
    }
}