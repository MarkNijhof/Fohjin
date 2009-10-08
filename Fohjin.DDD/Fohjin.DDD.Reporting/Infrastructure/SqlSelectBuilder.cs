using System;
using System.Linq;
using System.Collections.Generic;

namespace Fohjin.DDD.Reporting.Infrastructure
{
    public class SqlSelectBuilder
    {
        private readonly string[] _typesThatShouldNotBeWrappedInQuotes;
        
        public SqlSelectBuilder()
        {
            _typesThatShouldNotBeWrappedInQuotes = new[]
            {
                "int32", 
                "decimal",
                "float",
                "double",
            };
        }

        public string CreateSqlSelectStatementFromDto<TDto>() where TDto : class
        {
            return string.Format("{0};", GetSelectString<TDto>());
        }

        public string CreateSqlSelectStatementFromDto<TDto>(object example) where TDto : class
        {
            if (example == null)
                throw new ArgumentNullException("example");

            return string.Format("{0} {1};", GetSelectString<TDto>(), GetWhereString(example));
        }

        private static string GetSelectString<TDto>() 
        {
            var type = typeof(TDto);
            var properties = type.GetProperties();
            var tableName = type.Name;

            return string.Format("SELECT {0} FROM {1}", string.Join(",", properties.Select(x => x.Name).ToArray()), tableName);
        }

        private string GetWhereString(object example) 
        {
            var properties = example.GetType().GetProperties();

            var values = new Dictionary<string, string>();
            foreach (var property in properties)
            {
                var propertyValue = property.GetValue(example, new object[] {});
                if (propertyValue == null)
                    continue;

                values.Add(property.Name, ConvertPropertyValue(propertyValue));
            }

            return values.Count() > 0
                       ? string.Format("WHERE {0}", string.Join(" AND ", values.Select(x => string.Format("{0} = {1}", x.Key, x.Value)).ToArray()))
                       : string.Empty;
        }

        private string ConvertPropertyValue(object propertyValue)
        {
            return _typesThatShouldNotBeWrappedInQuotes.Contains(propertyValue.GetType().Name.ToLower()) 
                ? string.Format("{0}", propertyValue) 
                : string.Format("'{0}'", propertyValue);
        }
    }
}