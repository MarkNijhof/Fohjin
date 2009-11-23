using System;
using System.Linq;
using System.Reflection;

namespace Fohjin.DDD.Reporting.Infrastructure
{
    public interface ISqlUpdateBuilder
    {
        string GetUpdateString<TDto>(object update, object where) where TDto : class;
    }

    public class SqlUpdateBuilder : ISqlUpdateBuilder
    {
        public string GetUpdateString<TDto>(object update, object where) where TDto : class
        {
            if (update == null)
                throw new ArgumentNullException("update");

            if (where == null)
                throw new ArgumentNullException("where");

            var type = typeof(TDto);
            var updateProperties = update.GetType().GetProperties().Where(Where);
            var whereProperties = where.GetType().GetProperties().Where(Where);

            if (updateProperties.Count() == 0)
                throw new ArgumentNullException("update");

            if (whereProperties.Count() == 0)
                throw new ArgumentNullException("where");

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