using System;
using Fohjin.DDD.Reporting.Infrastructure;
using NUnit.Framework;


namespace Test.Fohjin.DDD.Reporting.Infrastructure
{
    [TestFixture]
    public class SqlUpdateBuilderTest
    {
        private SqlUpdateBuilder _sqlUpdateBuilder;

        [SetUp]
        public void SetUp()
        {
            _sqlUpdateBuilder = new SqlUpdateBuilder();
        }

        [Test]
        public void When_calling_CreateSqlSelectStatementFromDto_with_a_test_dto_it_will_generate_the_expected_sql_select_with_where_clause_statement_case_1()
        {
            Assert.That(_sqlUpdateBuilder.GetUpdateString<TestDtoCase1>(new { Column1 = "Test", Column2 = "Test" }, new { Column2 = "123" }),
                        Is.EqualTo("UPDATE TestDtoCase1 SET Column1=@update_column1,Column2=@update_column2 WHERE Column2=@column2;"));
        }

        [Test]
        [ExpectedException(typeof(ArgumentNullException))]
        public void When_calling_CreateSqlSelectStatementFromDto_with_a_null_update_object_will_throw_an_exception()
        {
            _sqlUpdateBuilder.GetUpdateString<TestDtoCase1>(null, new { Column2 = "123" });
        }

        [Test]
        [ExpectedException(typeof(ArgumentNullException))]
        public void When_calling_CreateSqlSelectStatementFromDto_with_a_null_where_object_will_throw_an_exception()
        {
            _sqlUpdateBuilder.GetUpdateString<TestDtoCase1>(new { Column2 = "123" }, null);
        }

        [Test]
        [ExpectedException(typeof(ArgumentNullException))]
        public void When_calling_CreateSqlSelectStatementFromDto_with_an_empty_update_object_will_throw_an_exception()
        {
            _sqlUpdateBuilder.GetUpdateString<TestDtoCase1>(new {}, new { Column2 = "123" });
        }

        [Test]
        [ExpectedException(typeof(ArgumentNullException))]
        public void When_calling_CreateSqlSelectStatementFromDto_with_an_empty_where_object_will_throw_an_exception()
        {
            _sqlUpdateBuilder.GetUpdateString<TestDtoCase1>(new { Column2 = "123" }, new { });
        }
    }
}