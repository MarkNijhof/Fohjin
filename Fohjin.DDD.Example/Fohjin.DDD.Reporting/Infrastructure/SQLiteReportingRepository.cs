using Microsoft.Data.Sqlite;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Reflection;

namespace Fohjin.DDD.Reporting.Infrastructure
{
    public class SqliteReportingRepository : IReportingRepository
    {
        private readonly string _sqLiteConnectionString;
        private readonly ISqlSelectBuilder _sqlSelectBuilder;
        private readonly ISqlInsertBuilder _sqlInsertBuilder;
        private readonly ISqlUpdateBuilder _sqlUpdateBuilder;
        private readonly ISqlDeleteBuilder _sqlDeleteBuilder;

        public SqliteReportingRepository(string sqLiteConnectionString, ISqlSelectBuilder sqlSelectBuilder, ISqlInsertBuilder sqlInsertBuilder, ISqlUpdateBuilder sqlUpdateBuilder, ISqlDeleteBuilder sqlDeleteBuilder)
        {
            _sqLiteConnectionString = sqLiteConnectionString;
            _sqlSelectBuilder = sqlSelectBuilder;
            _sqlInsertBuilder = sqlInsertBuilder;
            _sqlUpdateBuilder = sqlUpdateBuilder;
            _sqlDeleteBuilder = sqlDeleteBuilder;
        }

        public IEnumerable<TDto> GetByExample<TDto>(object? example) where TDto : class
        {
            return example == null
                ? GetByExample<TDto>(new Dictionary<string, object?>())
                : GetByExample<TDto>(GetPropertyInformation(example));
        }

        public IEnumerable<TDto> GetByExample<TDto>(IDictionary<string, object?> example) where TDto : class
        {
            List<TDto> dtos;
            var dtoType = typeof(TDto);

            using (var sqliteConnection = new SqliteConnection(_sqLiteConnectionString))
            {
                sqliteConnection.Open();

                using var sqliteTransaction = sqliteConnection.BeginTransaction();
                try
                {
                    dtos = DoGetByExample<TDto>(sqliteTransaction, dtoType, example);
                    GetChildren(sqliteTransaction, dtos, dtoType);
                    sqliteTransaction.Commit();
                }
                catch (Exception)
                {
                    sqliteTransaction.Rollback();
                    throw;
                }
            }
            return dtos;
        }

        public void Save<TDto>(TDto dto) where TDto : class
        {
            Save<TDto>(GetPropertyInformation(dto));
        }

        public void Save<TDto>(IEnumerable<KeyValuePair<string, object?>> dto) where TDto : class
        {
            var commandText = _sqlInsertBuilder.CreateSqlInsertStatementFromDto<TDto>();

            using var sqliteConnection = new SqliteConnection(_sqLiteConnectionString);
            sqliteConnection.Open();

            using var sqliteTransaction = sqliteConnection.BeginTransaction();
            try
            {
                using var sqliteCommand = new SqliteCommand(commandText, sqliteTransaction.Connection, sqliteTransaction);
                AddParameters(sqliteCommand, dto);
                sqliteCommand.ExecuteNonQuery();
                sqliteTransaction.Commit();
            }
            catch (Exception)
            {
                sqliteTransaction.Rollback();
                throw;
            }
        }

        public void Update<TDto>(object update, object where) where TDto : class
        {
            var commandText = _sqlUpdateBuilder.GetUpdateString<TDto>(update, where);

            using var sqliteConnection = new SqliteConnection(_sqLiteConnectionString);
            sqliteConnection.Open();

            using var sqliteTransaction = sqliteConnection.BeginTransaction();
            try
            {
                using var sqliteCommand = new SqliteCommand(commandText, sqliteTransaction.Connection, sqliteTransaction);
                AddUpdateParameters(sqliteCommand, GetPropertyInformation(update));
                AddParameters(sqliteCommand, GetPropertyInformation(where));
                sqliteCommand.ExecuteNonQuery();
                sqliteTransaction.Commit();
            }
            catch (Exception)
            {
                sqliteTransaction.Rollback();
                throw;
            }
        }

        public void Delete<TDto>(object example) where TDto : class
        {
            Delete<TDto>(GetPropertyInformation(example));
        }

        public void Delete<TDto>(IEnumerable<KeyValuePair<string, object?>> example) where TDto : class
        {
            var commandText = _sqlDeleteBuilder.CreateSqlDeleteStatementFromDto<TDto>(example);

            using var sqliteConnection = new SqliteConnection(_sqLiteConnectionString);
            sqliteConnection.Open();

            using var sqliteTransaction = sqliteConnection.BeginTransaction();
            try
            {
                using var sqliteCommand = new SqliteCommand(commandText, sqliteTransaction.Connection, sqliteTransaction);
                AddParameters(sqliteCommand, example);
                sqliteCommand.ExecuteNonQuery();
                sqliteTransaction.Commit();
            }
            catch (Exception)
            {
                sqliteTransaction.Rollback();
                throw;
            }
        }

        private void GetChildren<TDto>(SqliteTransaction sqliteTransaction, IEnumerable<TDto> dtos, Type dtoType) where TDto : class
        {
            foreach (var property in dtoType.GetProperties().Where(WhereGeneric))
            {
                foreach (var dto in dtos)
                {
                    var childDtoType = property.PropertyType.GetGenericArguments().First();

                    var childDtos = GetType()
                        .GetMethod("DoGetByExample", BindingFlags.NonPublic | BindingFlags.Instance)
                        ?.MakeGenericMethod(childDtoType)
                        .Invoke(this, new[] { sqliteTransaction, childDtoType, CreateSelectObject(dto) as object });

                    property.SetValue(dto, childDtos, Array.Empty<object>());
                }
            }
        }

        private static IEnumerable<KeyValuePair<string, object>> CreateSelectObject<TDto>(TDto parentDto)
        {
            if (parentDto == null)
                yield break;

            var columnName = $"{parentDto.GetType().Name}Id";
            var columnValue = parentDto.GetType().GetProperty("Id")?.GetValue(parentDto, Array.Empty<object>());

            if (columnValue == null)
                yield break;

            yield return KeyValuePair.Create(columnName, columnValue);
        }

        private List<TDto> DoGetByExample<TDto>(SqliteTransaction sqliteTransaction, Type dtoType, IEnumerable<KeyValuePair<string, object?>>? example) where TDto : class
        {
            var dtos = new List<TDto>();
            var commandText = _sqlSelectBuilder.CreateSqlSelectStatementFromDto<TDto>(example);

            using var sqliteCommand = new SqliteCommand(commandText, sqliteTransaction.Connection, sqliteTransaction);
            AddParameters(sqliteCommand, example);

            using var sqLiteDataReader = sqliteCommand.ExecuteReader();

            var dtoConstructor = dtoType.GetConstructors()
                    .Where(c => c.GetCustomAttribute<SqliteConstructorAttribute>() != null)
                    .FirstOrDefault() ?? throw new ApplicationException($"must label ctor for sqlite");

            while (sqLiteDataReader.Read())
            {
                dtos.Add(BuildDto<TDto>(dtoType, dtoConstructor, sqLiteDataReader));
            }
            return dtos;
        }

        private static TDto BuildDto<TDto>(Type dtoType, ConstructorInfo dtoConstructor, IDataRecord sqLiteDataReader) where TDto : class
        {
            var parameters = dtoConstructor.GetParameters();
            var parameterNames = dtoConstructor.GetParameters().Select(p => p.Name?.ToUpper()).ToArray();
            var constructorArguments = new object?[parameters.Length];

            foreach (var property in dtoType.GetProperties().Where(Where))
            {
                var index = Array.IndexOf(parameterNames, property.Name.ToUpper());
                if (index == -1)
                    continue;

                var value = sqLiteDataReader[property.Name];

                var converter = TypeDescriptor.GetConverter(parameters[index].ParameterType);
                if (converter.CanConvertFrom(value.GetType()))
                {
                    constructorArguments[index] = converter.ConvertFrom(value);
                }
                else
                {
                    converter = TypeDescriptor.GetConverter(value.GetType());
                    if (converter.CanConvertTo(parameters[index].ParameterType))
                    {
                        constructorArguments[index] = converter.ConvertTo(value, parameters[index].ParameterType);
                    }
                    else
                    {
                        if (parameters[index].ParameterType == typeof(decimal))
                        {
                            constructorArguments[index] = Convert.ToDecimal(value);
                        }
                        else
                        {
                            Debug.WriteLine($"Type conversion not supported {value.GetType()} -> {parameters[index].ParameterType}");
                        }
                    }
                }
            }

            return (TDto)dtoConstructor.Invoke(constructorArguments);
        }

        private static Dictionary<string, object?> GetPropertyInformation(object example) =>
            example.GetType().GetProperties()
                .Where(Where)
                .ToDictionary(x => x.Name, x => x.GetValue(example, Array.Empty<object>()));

        private static void AddParameters(SqliteCommand sqliteCommand, IEnumerable<KeyValuePair<string, object?>>? example)
        {
            if (example == null)
                return;
            foreach (var item in example)
                sqliteCommand.Parameters.Add(new SqliteParameter($"{@item.Key.ToLower()}", item.Value));
        }

        private static void AddUpdateParameters(SqliteCommand sqliteCommand, IEnumerable<KeyValuePair<string, object?>> example)
        {
            if (example == null)
                return;
            foreach (var item in example)
                sqliteCommand.Parameters.Add(new SqliteParameter($"@update_{item.Key.ToLower()}", item.Value));
        }

        private static bool Where(PropertyInfo propertyInfo) => !propertyInfo.PropertyType.IsGenericType;
        private static bool WhereGeneric(PropertyInfo propertyInfo) => propertyInfo.PropertyType.IsGenericType;
    }
}