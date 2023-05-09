using Fohjin.DDD.Reporting.Dtos;
using Fohjin.DDD.Reporting.Infrastructure;
using Microsoft.Data.Sqlite;
using System.Data.Common;

namespace Fohjin.DDD.BankApplication
{
    public class ReportingDatabaseBootStrapper
    {
        public const string dataBaseFile = "reportingDataBase.db3";
        private readonly List<Type> _dtos = new()
        {
            typeof(ClientReport),
            typeof(ClientDetailsReport),
            typeof(AccountReport),
            typeof(AccountDetailsReport),
            typeof(ClosedAccountReport),
            typeof(ClosedAccountDetailsReport),
            typeof(LedgerReport),
        };
        private readonly SqlCreateBuilder _sqlCreateBuilder = new ();

        public void ReCreateDatabaseSchema()
        {
            if (File.Exists(dataBaseFile))
                File.Delete(dataBaseFile);

            DoCreateDatabaseSchema();
        }

        public void CreateDatabaseSchemaIfNeeded()
        {
            if (File.Exists(dataBaseFile))
                return;

            DoCreateDatabaseSchema();
        }

        private void DoCreateDatabaseSchema()
        {
            //SQLiteConnection.CreateFile(dataBaseFile);

            var sqLiteConnection = new SqliteConnection(string.Format("Data Source={0}", dataBaseFile));

            sqLiteConnection.Open();

            using (DbTransaction dbTrans = sqLiteConnection.BeginTransaction())
            {
                using (DbCommand sqLiteCommand = sqLiteConnection.CreateCommand())
                {
                    foreach (var dto in _dtos)
                    {
                        sqLiteCommand.CommandText = _sqlCreateBuilder.CreateSqlCreateStatementFromDto(dto);
                        sqLiteCommand.ExecuteNonQuery();
                    }
                }
                dbTrans.Commit();
            }

            sqLiteConnection.Close();
        }
    }
}