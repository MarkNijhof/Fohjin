using System.Collections.Generic;
using Fohjin.DDD.Reporting.Infrastructure;
using NUnit.Framework;


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
        public void When_calling_CreateSqlSelectStatementFromDto_with_a_test_dto_and_null_example_it_will_fall_back_to_select_witout_a_where_clause()
        {
            Assert.That(_sqlSelectBuilder.CreateSqlSelectStatementFromDto<TestDtoCase1>(null),
                Is.EqualTo("SELECT Column1,Column2,Column3 FROM TestDtoCase1;"));
        }

        [Test]
        public void When_calling_CreateSqlSelectStatementFromDto_with_a_test_dto_it_will_generate_the_expected_sql_select_with_where_clause_statement_case_1()
        {
            var dictionary = new Dictionary<string, object> {{"Column1", "Test2"}, {"Column2", "Test1"}};

            Assert.That(_sqlSelectBuilder.CreateSqlSelectStatementFromDto<TestDtoCase1>(dictionary),
                Is.EqualTo("SELECT Column1,Column2,Column3 FROM TestDtoCase1 WHERE Column1 = @column1 AND Column2 = @column2;"));
        }

        [Test]
        public void When_calling_CreateSqlSelectStatementFromDto_with_a_test_dto_it_will_generate_the_expected_sql_select_statement_case_4()
        {
            Assert.That(_sqlSelectBuilder.CreateSqlSelectStatementFromDto<TestDtoCase4>(),
                Is.EqualTo("SELECT Column1,Column3 FROM TestDtoCase4;"));
        }
    }
}