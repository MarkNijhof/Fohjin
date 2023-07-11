using System.Reflection;

namespace Fohjin.DDD.Reporting.Infrastructure
{

    public class SqlSelectBuilder : ISqlSelectBuilder
    {
        public string CreateSqlSelectStatementFromDto<TDto>()
        {
            return string.Format("{0};", GetSelectString<TDto>());
        }

        public string CreateSqlSelectStatementFromDto<TDto>(IEnumerable<KeyValuePair<string, object?>>? example) where TDto : class
        {
            return example != null
                ? string.Format("{0} {1};", GetSelectString<TDto>(), GetWhereString(example))
                : string.Format("{0};", GetSelectString<TDto>());
        }

        private static string GetSelectString<TDto>()
        {
            var type = typeof(TDto);
            var properties = type.GetProperties();
            var tableName = type.Name;

            return string.Format("SELECT {0} FROM {1}", string.Join(",", properties.Where(Where).Select(x => x.Name).ToArray()), tableName);
        }

        private static bool Where(PropertyInfo propertyInfo)
        {
            return !propertyInfo.PropertyType.IsGenericType;
        }

        private static string GetWhereString(IEnumerable<KeyValuePair<string, object?>>? example)
        {
            return example?.Any() ?? false
                       ? string.Format("WHERE {0}", string.Join(" AND ", example.Select(x => string.Format("{0} = @{1}", x.Key, x.Key.ToLower())).ToArray()))
                       : string.Empty;
        }
    }
}