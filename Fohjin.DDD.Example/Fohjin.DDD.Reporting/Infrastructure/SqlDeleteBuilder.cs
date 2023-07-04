namespace Fohjin.DDD.Reporting.Infrastructure
{

    public class SqlDeleteBuilder : ISqlDeleteBuilder
    {
        public string CreateSqlDeleteStatementFromDto<TDto>() =>
            $"{GetDeleteString<TDto>()};";

        public string CreateSqlDeleteStatementFromDto<TDto>(IEnumerable<KeyValuePair<string, object?>>? example) where TDto : class =>
            example != null
                ? $"{GetDeleteString<TDto>()} {GetWhereString(example)};"
                : $"{GetDeleteString<TDto>()};";

        private static string GetDeleteString<TDto>() => $"DELETE FROM {typeof(TDto).Name}";

        private static string GetWhereString(IEnumerable<KeyValuePair<string, object?>> example) =>
            example.Any()
                ? $"WHERE {(string.Join(" AND ", example.Select(x => $"{x.Key} = @{x.Key.ToLower()}")))}"
                : string.Empty;
    }
}