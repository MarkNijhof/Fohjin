using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using Fohjin.DDD.EventStore.Storage;
using Fohjin.DDD.EventStore.Storage.Memento;

namespace Fohjin.DDD.EventStore.SQLite
{
    public class DomainEventStorage<TDomainEvent> : IDomainEventStorage<TDomainEvent> where TDomainEvent : IDomainEvent
    {
        private bool _isRunningWithinTransaction;
        private readonly string _sqLiteConnectionString;
        private readonly IFormatter _formatter;
        private SQLiteTransaction _sqLiteTransaction;
        private SQLiteConnection _sqliteConnection;

        public DomainEventStorage(string sqLiteConnectionString, IFormatter formatter)
        {
            _sqLiteConnectionString = sqLiteConnectionString;
            _formatter = formatter;
        }

        public IEnumerable<TDomainEvent> GetAllEvents(Guid eventProviderId)
        {
            const string commandText = @"SELECT Event FROM Events WHERE EventProviderId = @eventProviderId ORDER BY Version ASC;";

            var domainEvents = new List<TDomainEvent>();

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
                                    domainEvents.Add(Deserialize<TDomainEvent>((byte[])sqLiteDataReader["Event"]));
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

        public IEnumerable<TDomainEvent> GetEventsSinceLastSnapShot(Guid eventProviderId)
        {
            var snapShot = GetSnapShot(eventProviderId);

            var snapShotVersion = snapShot != null
                                 ? snapShot.Version
                                 : -1;

            var commandText = string.Format(@"SELECT Event FROM Events WHERE EventProviderId = @eventProviderId AND Version > {0} ORDER BY Version ASC;", snapShotVersion);

            var domainEvents = new List<TDomainEvent>();

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
                                    domainEvents.Add(Deserialize<TDomainEvent>((byte[])sqLiteDataReader["Event"]));
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

        public void Save(IEventProvider<TDomainEvent> eventProvider)
        {
            if (!_isRunningWithinTransaction)
                throw new Exception("Opperation is not running within a transaction");

            var version = GetEventProviderVersion(eventProvider, _sqLiteTransaction);

            if (version != eventProvider.Version)
                throw new ConcurrencyViolationException();

            foreach (var domainEvent in eventProvider.GetChanges())
            {
                SaveEvent(domainEvent, eventProvider, _sqLiteTransaction);
            }

            eventProvider.UpdateVersion(eventProvider.Version + eventProvider.GetChanges().Count());
            UpdateEventProviderVersion(eventProvider, _sqLiteTransaction);
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

        public void SaveShapShot(IEventProvider<TDomainEvent> entity)
        {
            StoreSnapShot(new SnapShot(entity.Id, entity.Version, ((IOrginator)entity).CreateMemento()));
        }

        public void BeginTransaction()
        {
            _sqliteConnection = new SQLiteConnection(_sqLiteConnectionString);
            _sqliteConnection.Open();
            _sqLiteTransaction = _sqliteConnection.BeginTransaction();
            _isRunningWithinTransaction = true;
        }

        public void Commit()
        {
            _isRunningWithinTransaction = false;
            _sqLiteTransaction.Commit();
            _sqLiteTransaction.Dispose();
            _sqliteConnection.Close();
            _sqliteConnection.Dispose();
        }

        public void Rollback()
        {
            _isRunningWithinTransaction = false;
            _sqLiteTransaction.Rollback();
            _sqLiteTransaction.Dispose();
            _sqliteConnection.Close();
            _sqliteConnection.Dispose();
        }

        private void SaveEvent(TDomainEvent domainEvent, IEventProvider<TDomainEvent> eventProvider, SQLiteTransaction transaction)
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

        private static void UpdateEventProviderVersion(IEventProvider<TDomainEvent> eventProvider, SQLiteTransaction transaction)
        {
            const string commandText = "UPDATE EventProviders SET Version = @version WHERE EventProviderId = @eventProviderId;";
            using (var sqLiteCommand = new SQLiteCommand(commandText, transaction.Connection, transaction))
            {
                sqLiteCommand.Parameters.Add(new SQLiteParameter("@eventProviderId", eventProvider.Id));
                sqLiteCommand.Parameters.Add(new SQLiteParameter("@version", eventProvider.Version));

                sqLiteCommand.ExecuteNonQuery();
            }
        }

        private static int GetEventProviderVersion(IEventProvider<TDomainEvent> eventProvider, SQLiteTransaction transaction)
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
}