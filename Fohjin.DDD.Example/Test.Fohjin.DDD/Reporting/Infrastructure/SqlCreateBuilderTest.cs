using Fohjin.DDD.Reporting.Infrastructure;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NUnit.Framework.SyntaxHelpers;

namespace Test.Fohjin.DDD.Reporting.Infrastructure
{
    [TestClass]
    public class SqlCreateBuilderTest
    {
        private SqlCreateBuilder _sqlCreateBuilder;

        [SetUp]
        public void SetUp()
        {
            _sqlCreateBuilder = new SqlCreateBuilder();
        }

        [TestMethod]
        public void When_calling_CreateSqlCreateStatementFromDto_with_a_test_dto_it_will_generate_the_expected_sql_create_statement_case_1()
        {
            Assert.AreEqual(_sqlCreateBuilder.CreateSqlCreateStatementFromDto(typeof(TestDtoCase1)),
                Is.EqualTo("CREATE TABLE TestDtoCase1 ([Column1] [nvarchar(250)],[Column2] [nvarchar(250)],[Column3] [nvarchar(250)]);"));
        }

        [TestMethod]
        public void When_calling_CreateSqlCreateStatementFromDto_with_a_test_dto_it_will_generate_the_expected_sql_create_statement_case_2()
        {
            Assert.AreEqual(_sqlCreateBuilder.CreateSqlCreateStatementFromDto(typeof(TestDtoCase2)),
                Is.EqualTo("CREATE TABLE TestDtoCase2 ([Column1] [int],[Column2] [numeric],[Column3] [numeric]);"));
        }

        [TestMethod]
        public void When_calling_CreateSqlCreateStatementFromDto_with_a_test_dto_it_will_generate_the_expected_sql_create_statement_case_3()
        {
            Assert.AreEqual(_sqlCreateBuilder.CreateSqlCreateStatementFromDto(typeof(TestDtoCase3)),
                Is.EqualTo("CREATE TABLE TestDtoCase3 ([Id] [uniqueidentifier] primary key,[Column1] [uniqueidentifier]);"));
        }

        [TestMethod]
        public void When_calling_CreateSqlCreateStatementFromDto_with_a_test_dto_it_will_generate_the_expected_sql_create_statement_case_4()
        {
            Assert.AreEqual(_sqlCreateBuilder.CreateSqlCreateStatementFromDto(typeof(TestDtoCase4)),
                Is.EqualTo("CREATE TABLE TestDtoCase4 ([Column1] [nvarchar(250)],[Column3] [nvarchar(250)]);"));
        }
    }
}