using System;
using System.Data.Common;
using System.Data.SQLite;

namespace Fohjin.DDD.Domain.Repositories
{
    public class SnapShotStorage : ISnapShotStorage 
    {
        private readonly SQLiteConnection _sqLiteConnection;

        public SnapShotStorage(SQLiteConnection sqLiteConnection)
        {
            _sqLiteConnection = sqLiteConnection;
        }

        public void Add(Guid id, ISnapShot snapShot)
        {
            using (DbTransaction dbTrans = _sqLiteConnection.BeginTransaction())
            {
                using (DbCommand sqLiteCommand = _sqLiteConnection.CreateCommand())
                {
                    //sqLiteCommand.CommandText = "INSERT INTO TestCase(MyValue) VALUES(?)";
                    //DbParameter Field1 = sqLiteCommand.CreateParameter();
                    //sqLiteCommand.Parameters.Add(Field1);
                    //for (int n = 0; n < 100000; n++)
                    //{
                    //    Field1.Value = n + 100000;
                    //    sqLiteCommand.ExecuteNonQuery();
                    //}
                }
                dbTrans.Commit();
            }
        }

        public bool HasSnapShots()
        {
            throw new NotImplementedException();
        }

        public ISnapShot GetLastSnapShot()
        {
            throw new NotImplementedException();
        }
    }
}