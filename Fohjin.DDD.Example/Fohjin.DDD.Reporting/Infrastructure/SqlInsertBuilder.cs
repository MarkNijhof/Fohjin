using System.Linq;
using System.Reflection;

namespace Fohjin.DDD.Reporting.Infrastructure
{
    public interface ISqlInsertBuilder
    {
        string CreateSqlInsertStatementFromDto<TDto>() where TDto : class;
    }

    public class SqlInsertBuilder : ISqlInsertBuilder
    {
        public string CreateSqlInsertStatementFromDto<TDto>() where TDto : class
        {
            return GetInsertString<TDto>();
        }

        private static string GetInsertString<TDto>()
        {
            var type = typeof(TDto);
            var properties = type.GetProperties().Where(Where);
            var tableName = type.Name;

            return string.Format("INSERT INTO {0} ({1}) VALUES ({2});", 
                tableName, 
                string.Join(",", properties.Select(x => x.Name).ToArray()),
                string.Join(",", properties.Select(x => string.Format("@{0}", x.Name.ToLower())).ToArray()));
        }

        private static bool Where(PropertyInfo propertyInfo)
        {
            return !propertyInfo.PropertyType.IsGenericType;
        }
    }
}