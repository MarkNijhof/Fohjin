using System;
using System.Linq;
using System.Collections.Generic;
using System.Reflection;

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

        public string CreateSqlSelectStatementFromDto<TDto>()
        {
            return string.Format("{0};", GetSelectString<TDto>());
        }

        public string CreateSqlSelectStatementFromDto<TDto>(object example)
        {
            if (example == null)
                throw new ArgumentNullException("example");

            var selectString = GetSelectString<TDto>();

            string whereString = GetWhereString(example);
            return string.Format("{0} {1};", selectString, whereString).Trim();
        }

        private static string GetSelectString<TDto>() 
        {
            var type = typeof(TDto);
            var properties = type.GetProperties();

            var tableName = type.Name;
            var columns = properties.Select(x => x.Name);

            var selectString = string.Format("SELECT {0} FROM {1}", string.Join(",", columns.ToArray()), tableName);

            return selectString;
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

            var whereString = "";
            if (values.Count() > 0)
            {
                whereString = string.Format("WHERE {0}", string.Join(" AND ", values.Select(x => string.Format("{0} = {1}", x.Key, x.Value)).ToArray()));
            }
            return whereString;
        }

        private string ConvertPropertyValue(object propertyValue)
        {
            if (_typesThatShouldNotBeWrappedInQuotes.Contains(propertyValue.GetType().Name.ToLower()))
                return string.Format("{0}", propertyValue);

            return string.Format("'{0}'", propertyValue);
        }
    }
}