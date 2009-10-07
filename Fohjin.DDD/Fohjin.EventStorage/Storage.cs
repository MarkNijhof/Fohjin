using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using Fohjin.DDD.Domain;
using Fohjin.DDD.Domain.Entities.Mementos;
using Fohjin.DDD.Events;

namespace Fohjin.EventStorage
{
    public class Storage : IDomainEventStorage, ISnapShotStorage
    {
        private const int snapShotInterval = 10;
        private readonly string _sqLiteConnectionString;
        private readonly IFormatter _formatter;

        public Storage(string sqLiteConnectionString, IFormatter formatter)
        {
            _sqLiteConnectionString = sqLiteConnectionString;
            _formatter = formatter;
        }

        public IEnumerable<IDomainEvent> GetAllEvents(Guid eventProviderId)
        {
            const string commandText = @"SELECT Event FROM Events WHERE EventProviderId = @eventProviderId ORDER BY Version ASC;";

            var domainEvents = new List<IDomainEvent>();

            using (var sqliteConnection = new SQLiteConnection(_sqLiteConnectionString))
            {
                sqliteConnection.Open();

                using (var sqliteTransaction = sqliteConnection.BeginTransaction())
                {
                    try
                    {
                        using (var sqliteCommand = new SQLiteCommand(commandText, sqliteTransaction.Connection, sqliteTransaction))
                        {
                            sqliteCommand.Parameters.Add(new SQLiteParameter("@eventProviderId", eventProviderId));
                            using (var sqLiteDataReader = sqliteCommand.ExecuteReader())
                            {
                                while (sqLiteDataReader.Read())
                                {
                                    domainEvents.Add(Deserialize<IDomainEvent>((byte[])sqLiteDataReader["Event"]));
                                }
                            }
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
            return domainEvents;
        }

        public IEnumerable<IDomainEvent> GetEventsSinceLastSnapShot(Guid eventProviderId)
        {
            var snapShot = GetSnapShot(eventProviderId);

            var snapShotVersion = snapShot != null
                                 ? snapShot.Version
                                 : -1;

            var commandText = string.Format(@"SELECT Event FROM Events WHERE EventProviderId = @eventProviderId AND Version > {0} ORDER BY Version ASC;", snapShotVersion);

            var domainEvents = new List<IDomainEvent>();

            using (var sqliteConnection = new SQLiteConnection(_sqLiteConnectionString))
            {
                sqliteConnection.Open();

                using (var sqliteTransaction = sqliteConnection.BeginTransaction())
                {
                    try
                    {
                        using (var sqliteCommand = new SQLiteCommand(commandText, sqliteTransaction.Connection, sqliteTransaction))
                        {
                            sqliteCommand.Parameters.Add(new SQLiteParameter("@eventProviderId", eventProviderId));
                            using (var sqLiteDataReader = sqliteCommand.ExecuteReader())
                            {
                                while (sqLiteDataReader.Read())
                                {
                                    domainEvents.Add(Deserialize<IDomainEvent>((byte[])sqLiteDataReader["Event"]));
                                }
                            }
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
            return domainEvents;
        }

        public int GetEventCountSinceLastSnapShot(Guid eventProviderId)
        {
            int count;
            var snapShot = GetSnapShot(eventProviderId);

            var snapShotVersion = snapShot != null
                                 ? snapShot.Version
                                 : 0;

            var commandText = string.Format(@"SELECT COUNT(*) FROM Events WHERE EventProviderId = @eventProviderId AND Version > {0};", snapShotVersion);

            using (var sqliteConnection = new SQLiteConnection(_sqLiteConnectionString))
            {
                sqliteConnection.Open();

                using (var sqliteTransaction = sqliteConnection.BeginTransaction())
                {
                    try
                    {
                        using (var sqliteCommand = new SQLiteCommand(commandText, sqliteTransaction.Connection, sqliteTransaction))
                        {
                            sqliteCommand.Parameters.Add(new SQLiteParameter("@eventProviderId", eventProviderId));
                            count = Convert.ToInt32(sqliteCommand.ExecuteScalar());
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
            return count;
        }

        public void Save(IEventProvider eventProvider)
        {
            using (var sqliteConnection = new SQLiteConnection(_sqLiteConnectionString))
            {
                sqliteConnection.Open();

                using (var sqliteTransaction = sqliteConnection.BeginTransaction())
                {
                    try
                    {
                        var version = GetEventProviderVersion(eventProvider, sqliteTransaction);

                        if (version != eventProvider.Version)
                            throw new ConcurrencyViolationException();

                        foreach (IDomainEvent domainEvent in eventProvider.GetChanges())
                        {
                            SaveEvent(domainEvent, eventProvider, sqliteTransaction);
                        }

                        eventProvider.UpdateVersion(eventProvider.Version + eventProvider.GetChanges().Count());
                        UpdateEventProviderVersion(eventProvider, sqliteTransaction);

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

        private void SaveEvent(IDomainEvent domainEvent, IEventProvider eventProvider, SQLiteTransaction transaction)
        {
            const string commandText = "INSERT INTO Events VALUES(@eventId, @eventProviderId, @event, @version)";
            using (var sqLiteCommand = new SQLiteCommand(commandText, transaction.Connection, transaction))
            {
                sqLiteCommand.Parameters.Add(new SQLiteParameter("@eventId", domainEvent.Id));
                sqLiteCommand.Parameters.Add(new SQLiteParameter("@eventProviderId", eventProvider.Id));
                sqLiteCommand.Parameters.Add(new SQLiteParameter("@event", Serialize(domainEvent)));
                sqLiteCommand.Parameters.Add(new SQLiteParameter("@version", domainEvent.Version));

                sqLiteCommand.ExecuteNonQuery();
            }
        }

        private static void UpdateEventProviderVersion(IEventProvider eventProvider, SQLiteTransaction transaction)
        {
            const string commandText = "UPDATE EventProviders SET Version = @version WHERE EventProviderId = @eventProviderId;";
            using (var sqLiteCommand = new SQLiteCommand(commandText, transaction.Connection, transaction))
            {
                sqLiteCommand.Parameters.Add(new SQLiteParameter("@eventProviderId", eventProvider.Id));
                sqLiteCommand.Parameters.Add(new SQLiteParameter("@version", eventProvider.Version));

                sqLiteCommand.ExecuteNonQuery();
            }
        }

        private static int GetEventProviderVersion(IEventProvider eventProvider, SQLiteTransaction transaction)
        {
            const string commandText = @"
                INSERT OR IGNORE INTO EventProviders VALUES (@eventProviderId, @type, 0);
                SELECT Version FROM EventProviders WHERE EventProviderId = @eventProviderId";
            using (var sqLiteCommand = new SQLiteCommand(commandText, transaction.Connection, transaction))
            {
                sqLiteCommand.Parameters.Add(new SQLiteParameter("@eventProviderId", eventProvider.Id));
                sqLiteCommand.Parameters.Add(new SQLiteParameter("@type", eventProvider.GetType().FullName));
                sqLiteCommand.Parameters.Add(new SQLiteParameter("@version", eventProvider.Version));

                return Convert.ToInt32(sqLiteCommand.ExecuteScalar());
            }
        }

        public ISnapShot GetSnapShot(Guid eventProviderId)
        {
            ISnapShot snapshot = null;
            const string commandText = @"SELECT SnapShot FROM SnapShots WHERE EventProviderId = @eventProviderId AND Version != -1;";

            using (var sqliteConnection = new SQLiteConnection(_sqLiteConnectionString))
            {
                sqliteConnection.Open();

                using (var sqliteTransaction = sqliteConnection.BeginTransaction())
                {
                    try
                    {
                        using (var sqliteCommand = new SQLiteCommand(commandText, sqliteTransaction.Connection, sqliteTransaction))
                        {
                            sqliteCommand.Parameters.Add(new SQLiteParameter("@eventProviderId", eventProviderId));
                            var bytes = (byte[])sqliteCommand.ExecuteScalar();
                            if (bytes != null)
                                snapshot = Deserialize<ISnapShot>(bytes);
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
            return snapshot;
        }

        public void SaveShapShot(IEventProvider entity)
        {
            var eventCount = GetEventCountSinceLastSnapShot(entity.Id);
            if (eventCount < snapShotInterval)
                return;

            StoreSnapShot(new SnapShot(entity.Id, entity.GetChanges().Last().Version, ((IOrginator)entity).CreateMemento()));
        }

        private void StoreSnapShot(ISnapShot snapShot)
        {
            const string commandText = "INSERT OR REPLACE INTO SnapShots (EventProviderId, SnapShot, Version) VALUES (@eventProviderId, @snapShot, @version);";

            using (var sqliteConnection = new SQLiteConnection(_sqLiteConnectionString))
            {
                sqliteConnection.Open();

                using (var sqliteTransaction = sqliteConnection.BeginTransaction())
                {
                    try
                    {
                        using (var sqliteCommand = new SQLiteCommand(commandText, sqliteTransaction.Connection, sqliteTransaction))
                        {
                            sqliteCommand.Parameters.Add(new SQLiteParameter("@eventProviderId", snapShot.EventProviderId));
                            sqliteCommand.Parameters.Add(new SQLiteParameter("@snapShot", Serialize(snapShot)));
                            sqliteCommand.Parameters.Add(new SQLiteParameter("@version", snapShot.Version));

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

        private byte[] Serialize(object theObject)
        {
            using (var memoryStream = new MemoryStream())
            {
                _formatter.Serialize(memoryStream, theObject);
                return memoryStream.ToArray();
            }
        }
        private TType Deserialize<TType>(byte[] bytes)
        {
            using (var memoryStream = new MemoryStream(bytes))
            {
                return (TType)_formatter.Deserialize(memoryStream);
            }
        }
    }

    public class ConcurrencyViolationException : Exception {}
}