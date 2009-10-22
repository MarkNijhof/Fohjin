using System.Collections.Generic;
using System.Linq;

namespace Fohjin.DDD.Reporting.Infrastructure
{
    public interface ISqlDeleteBuilder
    {
        string CreateSqlDeleteStatementFromDto<TDto>();
        string CreateSqlDeleteStatementFromDto<TDto>(IEnumerable<KeyValuePair<string, object>> example) where TDto : class;
    }

    public class SqlDeleteBuilder : ISqlDeleteBuilder
    {
        public string CreateSqlDeleteStatementFromDto<TDto>() 
        {
            return string.Format("{0};", GetDeleteString<TDto>());
        }

        public string CreateSqlDeleteStatementFromDto<TDto>(IEnumerable<KeyValuePair<string, object>> example) where TDto : class
        {
            return example != null 
                       ? string.Format("{0} {1};", GetDeleteString<TDto>(), GetWhereString(example))
                       : string.Format("{0};", GetDeleteString<TDto>());
        }

        private static string GetDeleteString<TDto>() 
        {
            var type = typeof(TDto);
            var tableName = type.Name;

            return string.Format("DELETE FROM {0}", tableName);
        }

        private static string GetWhereString(IEnumerable<KeyValuePair<string, object>> example) 
        {
            return example.Count() > 0
                       ? string.Format("WHERE {0}", string.Join(" AND ", example.Select(x => string.Format("{0} = @{1}", x.Key, x.Key.ToLower())).ToArray()))
                       : string.Empty;
        }
    }
}