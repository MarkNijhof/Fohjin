using System;
using Fohjin.DDD.Reporting.Infrastructure;
using NUnit.Framework;
using NUnit.Framework.SyntaxHelpers;

namespace Test.Fohjin.DDD.Reporting.Infrastructure
{
    [TestFixture]
    public class SqlSelectBuilderTest
    {
        private SqlSelectBuilder _sqlSelectBuilder;

        [SetUp]
        public void SetUp()
        {
            _sqlSelectBuilder = new SqlSelectBuilder();
        }

        [Test]
        public void When_calling_CreateSqlSelectStatementFromDto_with_a_test_dto_it_will_generate_the_expected_sql_select_statement_case_1()
        {
            Assert.That(_sqlSelectBuilder.CreateSqlSelectStatementFromDto<TestDtoCase1>(), 
                Is.EqualTo("SELECT Column1,Column2,Column3 FROM TestDtoCase1;"));
        }

        [Test]
        public void When_calling_CreateSqlSelectStatementFromDto_with_a_test_dto_it_will_generate_the_expected_sql_select_statement_case_2()
        {
            Assert.That(_sqlSelectBuilder.CreateSqlSelectStatementFromDto<TestDtoCase2>(), 
                Is.EqualTo("SELECT Column1,Column2,Column3 FROM TestDtoCase2;"));
        }

        [Test]
        public void When_calling_CreateSqlSelectStatementFromDto_with_a_test_dto_it_will_generate_the_expected_sql_select_statement_case_3()
        {
            Assert.That(_sqlSelectBuilder.CreateSqlSelectStatementFromDto<TestDtoCase3>(), 
                Is.EqualTo("SELECT Column1,Column2 FROM TestDtoCase3;"));
        }

        [Test]
        [ExpectedException(typeof(ArgumentNullException))]
        public void When_calling_CreateSqlSelectStatementFromDto_with_a_test_dto_and_null_example_it_will_throw_an_argument_null_exceptoin()
        {
            _sqlSelectBuilder.CreateSqlSelectStatementFromDto<TestDtoCase1>(null);
        }

        [Test]
        public void When_calling_CreateSqlSelectStatementFromDto_with_a_test_dto_it_will_generate_the_expected_sql_select_with_where_clause_statement_case_1()
        {
            Assert.That(_sqlSelectBuilder.CreateSqlSelectStatementFromDto<TestDtoCase1>(new { Column1 = "Test2", Column2 = "Test1" }),
                Is.EqualTo("SELECT Column1,Column2,Column3 FROM TestDtoCase1 WHERE Column1 = 'Test2' AND Column2 = 'Test1';"));
        }

        [Test]
        public void When_calling_CreateSqlSelectStatementFromDto_with_a_test_dto_it_will_generate_the_expected_sql_select_with_where_clause_statement_case_2()
        {
            Assert.That(_sqlSelectBuilder.CreateSqlSelectStatementFromDto<TestDtoCase2>(new { Column1 = 123, Column2 = 12.3 }),
                Is.EqualTo("SELECT Column1,Column2,Column3 FROM TestDtoCase2 WHERE Column1 = 123 AND Column2 = 12,3;"));
        }

        [Test]
        public void When_calling_CreateSqlSelectStatementFromDto_with_a_test_dto_it_will_generate_the_expected_sql_select_with_where_clause_statement_case_3()
        {
            var guid = Guid.NewGuid();
            var dateTime = DateTime.Now;
            Assert.That(_sqlSelectBuilder.CreateSqlSelectStatementFromDto<TestDtoCase3>(new { Column1 = guid, Column2 = dateTime }),
                Is.EqualTo(string.Format("SELECT Column1,Column2 FROM TestDtoCase3 WHERE Column1 = '{0}' AND Column2 = '{1}'", guid, dateTime)));
        }
    }

    public class TestDtoCase1
    {
        public string Column1 { get; set; }
        public string Column2 { get; set; }
        public string Column3 { get; set; }
    }

    public class TestDtoCase2
    {
        public int Column1 { get; set; }
        public decimal Column2 { get; set; }
        public double Column3 { get; set; }
    }

    public class TestDtoCase3
    {
        public Guid Column1 { get; set; }
        public DateTime Column2 { get; set; }
    }
}