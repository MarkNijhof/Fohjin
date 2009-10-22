using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.Linq;
using System.Reflection;

namespace Fohjin.DDD.Reporting.Infrastructure
{
    public class SQLiteReportingRepository : IReportingRepository
    {
        private readonly string _sqLiteConnectionString;
        private readonly ISqlSelectBuilder _sqlSelectBuilder;
        private readonly ISqlInsertBuilder _sqlInsertBuilder;
        private readonly ISqlUpdateBuilder _sqlUpdateBuilder;
        private readonly ISqlDeleteBuilder _sqlDeleteBuilder;

        public SQLiteReportingRepository(string sqLiteConnectionString, ISqlSelectBuilder sqlSelectBuilder, ISqlInsertBuilder sqlInsertBuilder, ISqlUpdateBuilder sqlUpdateBuilder, ISqlDeleteBuilder sqlDeleteBuilder)
        {
            _sqLiteConnectionString = sqLiteConnectionString;
            _sqlSelectBuilder = sqlSelectBuilder;
            _sqlInsertBuilder = sqlInsertBuilder;
            _sqlUpdateBuilder = sqlUpdateBuilder;
            _sqlDeleteBuilder = sqlDeleteBuilder;
        }

        public IEnumerable<TDto> GetByExample<TDto>(object example) where TDto : class
        {
            return example == null 
                ? GetByExample<TDto>(new Dictionary<string, object>()) 
                : GetByExample<TDto>(GetPropertyInformation(example));
        }

        public IEnumerable<TDto> GetByExample<TDto>(IDictionary<string, object> example) where TDto : class
        {
            List<TDto> dtos;
            var dtoType = typeof(TDto);

            using (var sqliteConnection = new SQLiteConnection(_sqLiteConnectionString))
            {
                sqliteConnection.Open();

                using (var sqliteTransaction = sqliteConnection.BeginTransaction())
                {
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
            }
            return dtos;
        }

        public void Save<TDto>(TDto dto) where TDto : class
        {
            Save<TDto>(GetPropertyInformation(dto));
        }

        public void Save<TDto>(IEnumerable<KeyValuePair<string, object>> dto) where TDto : class
        {
            var commandText = _sqlInsertBuilder.CreateSqlInsertStatementFromDto<TDto>();

            using (var sqliteConnection = new SQLiteConnection(_sqLiteConnectionString))
            {
                sqliteConnection.Open();

                using (var sqliteTransaction = sqliteConnection.BeginTransaction())
                {
                    try
                    {
                        using (var sqliteCommand = new SQLiteCommand(commandText, sqliteTransaction.Connection, sqliteTransaction))
                        {
                            AddParameters(sqliteCommand, dto);
                            sqliteCommand.ExecuteNonQuery();
                        }
                        sqliteTransaction.Commit();
                    }
                    catch (Exception)
                    {
                        sqliteTransaction.Rollback();
                        throw;
                    }
                }
            }
        }

        public void Update<TDto>(object update, object where) where TDto : class
        {
            var commandText = _sqlUpdateBuilder.GetUpdateString<TDto>(update, where);

            using (var sqliteConnection = new SQLiteConnection(_sqLiteConnectionString))
            {
                sqliteConnection.Open();

                using (var sqliteTransaction = sqliteConnection.BeginTransaction())
                {
                    try
                    {
                        using (var sqliteCommand = new SQLiteCommand(commandText, sqliteTransaction.Connection, sqliteTransaction))
                        {
                            AddUpdateParameters(sqliteCommand, GetPropertyInformation(update));
                            AddParameters(sqliteCommand, GetPropertyInformation(where));
                            sqliteCommand.ExecuteNonQuery();
                        }
                        sqliteTransaction.Commit();
                    }
                    catch (Exception)
                    {
                        sqliteTransaction.Rollback();
                        throw;
                    }
                }
            }
        }

        public void Delete<TDto>(object example) where TDto : class
        {
            Delete<TDto>(GetPropertyInformation(example));
        }

        public void Delete<TDto>(IEnumerable<KeyValuePair<string, object>> example) where TDto : class
        {
            var commandText = _sqlDeleteBuilder.CreateSqlDeleteStatementFromDto<TDto>(example);

            using (var sqliteConnection = new SQLiteConnection(_sqLiteConnectionString))
            {
                sqliteConnection.Open();

                using (var sqliteTransaction = sqliteConnection.BeginTransaction())
                {
                    try
                    {
                        using (var sqliteCommand = new SQLiteCommand(commandText, sqliteTransaction.Connection, sqliteTransaction))
                        {
                            AddParameters(sqliteCommand, example);
                            sqliteCommand.ExecuteNonQuery();
                        }
                        sqliteTransaction.Commit();
                    }
                    catch (Exception)
                    {
                        sqliteTransaction.Rollback();
                        throw;
                    }
                }
            }
        }

        private void GetChildren<TDto>(SQLiteTransaction sqliteTransaction, IEnumerable<TDto> dtos, Type dtoType) where TDto : class
        {
            foreach (var property in dtoType.GetProperties().Where(WhereGeneric))
            {
                foreach (var dto in dtos)
                {
                    var childDtoType = property.PropertyType.GetGenericArguments().First();

                    var childDtos = GetType()
                        .GetMethod("DoGetByExample", BindingFlags.NonPublic | BindingFlags.Instance)
                        .MakeGenericMethod(childDtoType)
                        .Invoke(this, new[] { sqliteTransaction, childDtoType, CreateSelectObject(dto) as object });
                    
                    property.SetValue(dto, childDtos, new object[] { });
                }
            }
        }

        private static IEnumerable<KeyValuePair<string, object>> CreateSelectObject<TDto>(TDto parentDto)
        {
            var columnName = string.Format("{0}Id", parentDto.GetType().Name);
            var columnValue = parentDto.GetType().GetProperty("Id").GetValue(parentDto, new object[] {});
         
            return new Dictionary<string, object> { {columnName, columnValue} };
        }

        private List<TDto> DoGetByExample<TDto>(SQLiteTransaction sqliteTransaction, Type dtoType, IEnumerable<KeyValuePair<string, object>> example) where TDto : class
        {
            var dtos = new List<TDto>();
            var commandText = _sqlSelectBuilder.CreateSqlSelectStatementFromDto<TDto>(example);

            using (var sqliteCommand = new SQLiteCommand(commandText, sqliteTransaction.Connection, sqliteTransaction))
            {
                AddParameters(sqliteCommand, example);

                using (var sqLiteDataReader = sqliteCommand.ExecuteReader())
                {
                    var dtoConstructor = dtoType.GetConstructors().OrderBy(x => x.GetParameters().Count()).FirstOrDefault();

                    while (sqLiteDataReader.Read())
                    {
                        dtos.Add(BuildDto<TDto>(dtoType, dtoConstructor, sqLiteDataReader));
                    }
                }
            }
            return dtos;
        }

        private static TDto BuildDto<TDto>(Type dtoType, ConstructorInfo dtoConstructor, IDataRecord sqLiteDataReader) where TDto : class
        {
            var constructorArguments = new List<object>();

            dtoType.GetProperties().Where(Where).ToList().ForEach(x => constructorArguments.Add(sqLiteDataReader[x.Name]));

            return (TDto)dtoConstructor.Invoke(constructorArguments.ToArray());
        }

        private static Dictionary<string, object> GetPropertyInformation(object example)
        {
            var exampleData = new Dictionary<string, object>();

            example.GetType().GetProperties().Where(Where).ToList().ForEach(x => exampleData.Add(x.Name, x.GetValue(example, new object[] { })));
            return exampleData;
        }

        private static void AddParameters(SQLiteCommand sqliteCommand, IEnumerable<KeyValuePair<string, object>> example)
        {
            if (example == null)
                return;

            example.ToList().ForEach(x => sqliteCommand.Parameters.Add(new SQLiteParameter(string.Format("@{0}", x.Key.ToLower()), x.Value)));
        }

        private static void AddUpdateParameters(SQLiteCommand sqliteCommand, IEnumerable<KeyValuePair<string, object>> example)
        {
            if (example == null)
                return;

            example.ToList().ForEach(x => sqliteCommand.Parameters.Add(new SQLiteParameter(string.Format("@update_{0}", x.Key.ToLower()), x.Value)));
        }

        private static bool Where(PropertyInfo propertyInfo)
        {
            return !propertyInfo.PropertyType.IsGenericType;
        }

        private static bool WhereGeneric(PropertyInfo propertyInfo)
        {
            return propertyInfo.PropertyType.IsGenericType;
        }
    }
}