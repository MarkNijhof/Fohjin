using System;
using System.Data.Common;
using System.Data.SQLite;
using System.Linq;
using Fohjin.DDD.Domain;
using Fohjin.DDD.Domain.Contracts;
using Fohjin.DDD.Domain.Memento;
using Fohjin.DDD.Domain.Repositories;

namespace Fohjin.DDD.Infrastructure
{
    public class SnapShotStorage : ISnapShotStorage 
    {
        private const int snapShotInterval = 10;

        private readonly SQLiteConnection _sqLiteConnection;
        private readonly ISerializer _serializer;
        private readonly IDomainEventStorage _domainEventStorage;

        public SnapShotStorage(SQLiteConnection sqLiteConnection, ISerializer serializer, IDomainEventStorage domainEventStorage)
        {
            _sqLiteConnection = sqLiteConnection;
            _serializer = serializer;
            _domainEventStorage = domainEventStorage;
        }

        public void SaveShapShot(IExposeMyInternalChanges entity)
        {
            if (GetLastSnapShot(entity.Id) != null)
            {
                var events = _domainEventStorage.GetEventsSinceLastSnapShot(entity.Id);
                if (events.Count() < snapShotInterval)
                    return;
            }
            else
            {
                var events = _domainEventStorage.GetAllEvents(entity.Id);
                if (events.Count() < snapShotInterval)
                    return;
            }

            var orginator = (IOrginator)entity;
            Add(entity.Id, new SnapShot(entity.GetChanges().Last().Id, orginator.CreateMemento()));
        }

        public ISnapShot GetLastSnapShot(Guid entityId)
        {
            try
            {
                _sqLiteConnection.Open();

                using (DbTransaction dbTrans = _sqLiteConnection.BeginTransaction())
                {
                    using (DbCommand sqLiteCommand = _sqLiteConnection.CreateCommand())
                    {
                        sqLiteCommand.CommandText = @"SELECT SnapShot FROM SnapShots WHERE AggregateId = @AggregateId ORDER BY TimeStamp DESC LIMIT 1;";
                        var aggregateIdField = sqLiteCommand.CreateParameter();
                        aggregateIdField.ParameterName = "@AggregateId";
                        aggregateIdField.Value = entityId;

                        sqLiteCommand.Parameters.Add(aggregateIdField);

                        using (var reader = sqLiteCommand.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                return _serializer.Deserialize<ISnapShot>(reader[0].ToString());
                            }
                        }
                    }
                    dbTrans.Commit();
                }
            }
            finally
            {
                _sqLiteConnection.Close();
            }
            return null;
        }

        private void Add(Guid entityId, ISnapShot snapShot)
        {
            try
            {
                _sqLiteConnection.Open();

                using (DbTransaction dbTrans = _sqLiteConnection.BeginTransaction())
                {
                    using (DbCommand sqLiteCommand = _sqLiteConnection.CreateCommand())
                    {
                        sqLiteCommand.CommandText = @"INSERT INTO SnapShots (Id, AggregateId, SnapShot, TimeStamp) VALUES (@id, @aggregateId, @snapShot, @timeStamp);";
                        var idField = sqLiteCommand.CreateParameter();
                        idField.ParameterName = "@id";
                        idField.Value = Guid.NewGuid();
                        var aggregateIdField = sqLiteCommand.CreateParameter();
                        aggregateIdField.ParameterName = "@aggregateId";
                        aggregateIdField.Value = entityId;
                        var snapShotField = sqLiteCommand.CreateParameter();
                        snapShotField.ParameterName = "@snapShot";
                        snapShotField.Value = _serializer.Serialize(snapShot);
                        var timeStampField = sqLiteCommand.CreateParameter();
                        timeStampField.ParameterName = "@timeStamp";
                        timeStampField.Value = DateTime.Now;

                        sqLiteCommand.Parameters.Add(idField);
                        sqLiteCommand.Parameters.Add(aggregateIdField);
                        sqLiteCommand.Parameters.Add(snapShotField);
                        sqLiteCommand.Parameters.Add(timeStampField);

                        sqLiteCommand.ExecuteNonQuery();
                    }
                    dbTrans.Commit();
                }
            }
            finally
            {
                _sqLiteConnection.Close();
            }
        }
    }
}