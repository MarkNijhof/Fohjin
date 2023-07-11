namespace Fohjin.DDD.Reporting.Infrastructure
{
    public interface ISqlDeleteBuilder
    {
        string CreateSqlDeleteStatementFromDto<TDto>();
        string CreateSqlDeleteStatementFromDto<TDto>(IEnumerable<KeyValuePair<string, object?>> example) where TDto : class;
    }
}