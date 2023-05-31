namespace Fohjin.DDD.Reporting.Infrastructure
{
    public interface ISqlInsertBuilder
    {
        string CreateSqlInsertStatementFromDto<TDto>() where TDto : class;
    }
}