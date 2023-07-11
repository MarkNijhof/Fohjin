namespace Fohjin.DDD.Reporting.Infrastructure
{
    public interface ISqlCreateBuilder
    {
        string CreateSqlCreateStatementFromDto(Type dtoType);
    }
}