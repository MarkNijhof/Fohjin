namespace Fohjin.DDD.Reporting.Infrastructure
{
    public interface ISqlUpdateBuilder
    {
        string GetUpdateString<TDto>(object? update, object? where) where TDto : class;
    }
}