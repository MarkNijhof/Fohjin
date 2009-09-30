using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.SQLite;
using Fohjin.DDD.Domain.Contracts;
using Fohjin.DDD.Domain.Events;

namespace Fohjin.DDD.Infrastructure
{
    public class DomainEventStorage : IDomainEventStorage
    {
        private readonly SQLiteConnection _sqLiteConnection;
        private readonly ISerializer _serializer;

        public DomainEventStorage(SQLiteConnection sqLiteConnection, ISerializer serializer)
        {
            _sqLiteConnection = sqLiteConnection;
            _serializer = serializer;
        }

        public IEnumerable<IDomainEvent> GetAllEvents(Guid aggregateId)
        {
            var domainEvents = new List<IDomainEvent>();
            try
            {
                _sqLiteConnection.Open();

                using (DbTransaction dbTrans = _sqLiteConnection.BeginTransaction())
                {
                    using (DbCommand sqLiteCommand = _sqLiteConnection.CreateCommand())
                    {
                        sqLiteCommand.CommandText = @"SELECT Event FROM Events WHERE AggregateId = @AggregateId ORDER BY TimeStamp ASC;";
                        var aggregateIdField = sqLiteCommand.CreateParameter();
                        aggregateIdField.ParameterName = "@AggregateId";
                        aggregateIdField.Value = aggregateId;

                        sqLiteCommand.Parameters.Add(aggregateIdField);

                        using (var reader = sqLiteCommand.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                domainEvents.Add(_serializer.Deserialize<IDomainEvent>(reader[0].ToString()));
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
            return domainEvents;
        }

        public IEnumerable<IDomainEvent> GetEventsSinceLastSnapShot(Guid aggregateId)
        {
            var domainEvents = new List<IDomainEvent>();
            try
            {
                _sqLiteConnection.Open();

                using (DbTransaction dbTrans = _sqLiteConnection.BeginTransaction())
                {
                    using (DbCommand sqLiteCommand = _sqLiteConnection.CreateCommand())
                    {
                        sqLiteCommand.CommandText = @"
                            SELECT 
                                Event 
                            FROM 
                                Events 
                            WHERE 
                                AggregateId = @AggregateId AND 
                                TimeStamp > 
                                (
                                    SELECT 
                                        TimeStamp 
                                    FROM 
                                        SnapShots 
                                    WHERE 
                                        AggregateId = @AggregateId 
                                    ORDER BY TimeStamp DESC
                                    LIMIT 1
                                ) 
                            ORDER BY TimeStamp ASC;";
                        var aggregateIdField = sqLiteCommand.CreateParameter();
                        aggregateIdField.ParameterName = "@AggregateId";
                        aggregateIdField.Value = aggregateId;
                        sqLiteCommand.Parameters.Add(aggregateIdField);

                        using (var reader = sqLiteCommand.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                domainEvents.Add(_serializer.Deserialize<IDomainEvent>(reader[0].ToString()));
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
            return domainEvents;
        }

        public void SaveEvents(Guid id, IEnumerable<IDomainEvent> domainEvents)
        {
            try
            {
                _sqLiteConnection.Open();

                using (DbTransaction dbTrans = _sqLiteConnection.BeginTransaction())
                {
                    using (DbCommand sqLiteCommand = _sqLiteConnection.CreateCommand())
                    {
                        sqLiteCommand.CommandText = @"INSERT INTO Events (Id, AggregateId, Event, TimeStamp) VALUES (@id, @aggregateId, @event, @timeStamp);";
                        var idField = sqLiteCommand.CreateParameter();
                        idField.ParameterName = "@id";
                        var aggregateIdField = sqLiteCommand.CreateParameter();
                        aggregateIdField.ParameterName = "@aggregateId";
                        var eventField = sqLiteCommand.CreateParameter();
                        eventField.ParameterName = "@event";
                        var timeStampField = sqLiteCommand.CreateParameter();
                        timeStampField.ParameterName = "@timeStamp";

                        sqLiteCommand.Parameters.Add(idField);
                        sqLiteCommand.Parameters.Add(aggregateIdField);
                        sqLiteCommand.Parameters.Add(eventField);
                        sqLiteCommand.Parameters.Add(timeStampField);

                        foreach (var domainEvent in domainEvents)
                        {
                            idField.Value = domainEvent.Id;
                            aggregateIdField.Value = id;
                            eventField.Value = _serializer.Serialize(domainEvent);
                            timeStampField.Value = domainEvent.TimeStamp;
                            sqLiteCommand.ExecuteNonQuery();
                        }
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