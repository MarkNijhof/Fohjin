using System.Reflection;

namespace Fohjin.DDD.Reporting.Infrastructure
{

    public class SqlUpdateBuilder : ISqlUpdateBuilder
    {
        public string GetUpdateString<TDto>(object? update, object? where) where TDto : class
        {
            if (update == null)
                throw new ArgumentNullException(nameof(update));

            if (where == null)
                throw new ArgumentNullException(nameof(where));

            var type = typeof(TDto);
            var updateProperties = update.GetType().GetProperties().Where(Where);
            var whereProperties = where.GetType().GetProperties().Where(Where);

            if (!updateProperties.Any())
                throw new ArgumentNullException(nameof(update));

            if (!whereProperties.Any())
                throw new ArgumentNullException(nameof(where));

            var tableName = type.Name;

            return string.Format("UPDATE {0} SET {1} WHERE {2};",
                                 tableName,
                                 string.Join(",", updateProperties.Select(x => string.Format("{0}=@update_{1}", x.Name, x.Name.ToLower())).ToArray()),
                                 string.Join(",", whereProperties.Select(x => string.Format("{0}=@{1}", x.Name, x.Name.ToLower())).ToArray()));
        }

        private static bool Where(PropertyInfo propertyInfo)
        {
            return !propertyInfo.PropertyType.IsGenericType;
        }
    }
}