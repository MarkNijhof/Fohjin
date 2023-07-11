using Fohjin.DDD.Reporting.Infrastructure;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Test.Fohjin.DDD.Reporting.Infrastructure;

[TestClass]
public class SqlInsertBuilderTest
{
    private SqlInsertBuilder? _sqlInsertBuilder;

    [TestInitialize]
    public void SetUp()
    {
        _sqlInsertBuilder = new SqlInsertBuilder();
    }

    [TestMethod]
    public void When_calling_CreateSqlSelectStatementFromDto_with_a_test_dto_it_will_generate_the_expected_sql_select_with_where_clause_statement_case_1()
    {
        Assert.AreEqual(
            "INSERT INTO TestDtoCase1 (Column1,Column2,Column3) VALUES (@column1,@column2,@column3);",
            _sqlInsertBuilder?.CreateSqlInsertStatementFromDto<TestDtoCase1>());
    }

    [TestMethod]
    public void When_calling_CreateSqlSelectStatementFromDto_with_a_test_dto_it_will_generate_the_expected_sql_select_with_where_clause_statement_case_4()
    {
        Assert.AreEqual(
            "INSERT INTO TestDtoCase4 (Column1,Column3) VALUES (@column1,@column3);",
            _sqlInsertBuilder?.CreateSqlInsertStatementFromDto<TestDtoCase4>());
    }
}