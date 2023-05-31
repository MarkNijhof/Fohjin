using Microsoft.Data.Sqlite;
using System.Data.Common;

namespace Fohjin.DDD.BankApplication
{
    public class DomainDatabaseBootStrapper
    {
        public const string DataBaseFile = "domainDataBase.db3";

        public void ReCreateDatabaseSchema(string dataBaseFile)
        {
            if (File.Exists(dataBaseFile))
                File.Delete(dataBaseFile);

            DoCreateDatabaseSchema(dataBaseFile);
        }

        public void CreateDatabaseSchemaIfNeeded(string dataBaseFile)
        {
            if (File.Exists(dataBaseFile))
                return;

            DoCreateDatabaseSchema(dataBaseFile);
        }

        private static void DoCreateDatabaseSchema(string dataBaseFile)
        {
            //SqliteConnection.CreateFile(dataBaseFile);

            var sqLiteConnection = new SqliteConnection(string.Format("Data Source={0}", dataBaseFile));

            sqLiteConnection.Open();

            using (DbTransaction dbTrans = sqLiteConnection.BeginTransaction())
            {
                using (DbCommand sqLiteCommand = sqLiteConnection.CreateCommand())
                {
                    const string eventProvidersTables = @"
                        CREATE TABLE EventProviders
                        (
                            [EventProviderId] [uniqueidentifier] primary key,
                            [Type] [nvarchar(250)] not null,
                            [Version] [int] not null
                        );
                        ";
                    sqLiteCommand.CommandText = eventProvidersTables;
                    sqLiteCommand.ExecuteNonQuery();

                    const string eventsTables = @"
                        CREATE TABLE Events
                        (
                            [Id] [uniqueidentifier] primary key,
                            [EventProviderId] [uniqueidentifier] not null,
                            [Event] [binary] not null,
                            [Version] [int] not null
                        );
                        ";
                    sqLiteCommand.CommandText = eventsTables;
                    sqLiteCommand.ExecuteNonQuery();

                    const string snapshotsTables = @"
                        CREATE TABLE SnapShots
                        (
                            [EventProviderId] [uniqueidentifier] primary key,
                            [SnapShot] [binary] not null,
                            [Version] [int] not null
                        );
                        ";
                    sqLiteCommand.CommandText = snapshotsTables;
                    sqLiteCommand.ExecuteNonQuery();
                }
                dbTrans.Commit();
            }

            sqLiteConnection.Close();
        }
    }
}