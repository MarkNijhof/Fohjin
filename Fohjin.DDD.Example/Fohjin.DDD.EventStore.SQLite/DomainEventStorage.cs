using Fohjin.DDD.Common;
using Fohjin.DDD.EventStore.Storage;
using Fohjin.DDD.EventStore.Storage.Memento;
using Microsoft.Data.Sqlite;
using Microsoft.Extensions.Configuration;

namespace Fohjin.DDD.EventStore.SQLite
{
    public abstract class DomainEventStorage
    {
        public const string ConnectionStringConfigKey = "DomainEventStorage:SqliteConnectionString";
    }
    public class DomainEventStorage<TDomainEvent> : DomainEventStorage, IDomainEventStorage<TDomainEvent> where TDomainEvent : IDomainEvent
    {
        private bool _isRunningWithinTransaction;
        private readonly string _sqLiteConnectionString;
        private readonly IExtendedFormatter _formatter;
        private SqliteTransaction? _sqLiteTransaction;
        private SqliteConnection? _sqliteConnection;

        public DomainEventStorage(IConfiguration configuration, IExtendedFormatter formatter)
        {
            _sqLiteConnectionString = configuration[ConnectionStringConfigKey] ??
                throw new NotSupportedException($"configuration for {nameof(ConnectionStringConfigKey)} is missing");
            _formatter = formatter;
        }

        public IEnumerable<TDomainEvent> GetAllEvents(Guid eventProviderId)
        {
            const string commandText = @"SELECT Event FROM Events WHERE EventProviderId = @eventProviderId ORDER BY Version ASC;";

            var domainEvents = new List<TDomainEvent>();

            using var sqliteConnection = new SqliteConnection(_sqLiteConnectionString);
            sqliteConnection.Open();

            using var sqliteTransaction = sqliteConnection.BeginTransaction();
            try
            {
                using var sqliteCommand = new SqliteCommand(commandText, sqliteTransaction.Connection, sqliteTransaction);
                sqliteCommand.Parameters.Add(new SqliteParameter("@eventProviderId", eventProviderId));
                using var sqLiteDataReader = sqliteCommand.ExecuteReader();
                while (sqLiteDataReader.Read())
                {
                    domainEvents.Add(Deserialize<TDomainEvent>((byte[])sqLiteDataReader["Event"]));
                }
                sqliteTransaction.Commit();
            }
            catch (Exception)
            {
                sqliteTransaction.Rollback();
                throw;
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

            using var sqliteConnection = new SqliteConnection(_sqLiteConnectionString);
            sqliteConnection.Open();

            using var sqliteTransaction = sqliteConnection.BeginTransaction();
            try
            {
                using var sqliteCommand = new SqliteCommand(commandText, sqliteTransaction.Connection, sqliteTransaction);
                sqliteCommand.Parameters.Add(new SqliteParameter("@eventProviderId", eventProviderId));
                using var sqLiteDataReader = sqliteCommand.ExecuteReader();
                while (sqLiteDataReader.Read())
                {
                    domainEvents.Add(Deserialize<TDomainEvent>((byte[])sqLiteDataReader["Event"]));
                }
                sqliteTransaction.Commit();
            }
            catch (Exception)
            {
                sqliteTransaction.Rollback();
                throw;
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

            using var sqliteConnection = new SqliteConnection(_sqLiteConnectionString);
            sqliteConnection.Open();

            using var sqliteTransaction = sqliteConnection.BeginTransaction();
            try
            {
                using var sqliteCommand = new SqliteCommand(commandText, sqliteTransaction.Connection, sqliteTransaction);
                sqliteCommand.Parameters.Add(new SqliteParameter("@eventProviderId", eventProviderId));
                count = Convert.ToInt32(sqliteCommand.ExecuteScalar());
                sqliteTransaction.Commit();
            }
            catch (Exception)
            {
                sqliteTransaction.Rollback();
                throw;
            }
            return count;
        }

        public void Save(IEventProvider<TDomainEvent> eventProvider)
        {
            if (!_isRunningWithinTransaction || _sqLiteTransaction == null)
                throw new Exception("Operation is not running within a transaction");

            var version = GetEventProviderVersion(eventProvider, _sqLiteTransaction);

            if (version != eventProvider.Version && eventProvider.Version > 0)
                throw new ConcurrencyViolationException($"version not correct: {version} != {eventProvider.Version} ({eventProvider.GetType()})");

            foreach (var domainEvent in eventProvider.GetChanges())
            {
                SaveEvent(domainEvent, eventProvider, _sqLiteTransaction);
            }

            eventProvider.UpdateVersion(eventProvider.Version + eventProvider.GetChanges().Count());
            UpdateEventProviderVersion(eventProvider, _sqLiteTransaction);
        }

        public ISnapShot? GetSnapShot(Guid eventProviderId)
        {
            ISnapShot? snapshot = null;
            const string commandText = @"SELECT SnapShot FROM SnapShots WHERE EventProviderId = @eventProviderId AND Version != -1;";

            using var sqliteConnection = new SqliteConnection(_sqLiteConnectionString);
            sqliteConnection.Open();

            using var sqliteTransaction = sqliteConnection.BeginTransaction();
            try
            {
                using var sqliteCommand = new SqliteCommand(commandText, sqliteTransaction.Connection, sqliteTransaction);
                sqliteCommand.Parameters.Add(new SqliteParameter("@eventProviderId", eventProviderId));
                if (sqliteCommand.ExecuteScalar() is byte[] bytes)
                    snapshot = Deserialize<ISnapShot>(bytes);
                sqliteTransaction.Commit();
            }
            catch (Exception)
            {
                sqliteTransaction.Rollback();
                throw;
            }
            return snapshot;
        }

        public void SaveShapShot(IEventProvider<TDomainEvent> entity)
        {
            StoreSnapShot(new SnapShot(entity.Id, entity.Version, ((IOriginator)entity).CreateMemento()));
        }

        public void BeginTransaction()
        {
            _sqliteConnection = new SqliteConnection(_sqLiteConnectionString);
            _sqliteConnection.Open();
            _sqLiteTransaction = _sqliteConnection.BeginTransaction();
            _isRunningWithinTransaction = true;
        }

        public void Commit()
        {
            _isRunningWithinTransaction = false;
            _sqLiteTransaction?.Commit();
            _sqLiteTransaction?.Dispose();
            _sqliteConnection?.Close();
            _sqliteConnection?.Dispose();
        }

        public void Rollback()
        {
            _isRunningWithinTransaction = false;
            _sqLiteTransaction?.Rollback();
            _sqLiteTransaction?.Dispose();
            _sqliteConnection?.Close();
            _sqliteConnection?.Dispose();
        }

        private void SaveEvent(TDomainEvent domainEvent, IEventProvider<TDomainEvent> eventProvider, SqliteTransaction transaction)
        {
            const string commandText = "INSERT INTO Events VALUES(@eventId, @eventProviderId, @event, @version)";
            using var sqLiteCommand = new SqliteCommand(commandText, transaction.Connection, transaction);
            sqLiteCommand.Parameters.Add(new SqliteParameter("@eventId", domainEvent.Id));
            sqLiteCommand.Parameters.Add(new SqliteParameter("@eventProviderId", eventProvider.Id));
            sqLiteCommand.Parameters.Add(new SqliteParameter("@event", Serialize(domainEvent)));
            sqLiteCommand.Parameters.Add(new SqliteParameter("@version", domainEvent.Version));

            sqLiteCommand.ExecuteNonQuery();
        }

        private void StoreSnapShot(ISnapShot snapShot)
        {
            const string commandText = "INSERT OR REPLACE INTO SnapShots (EventProviderId, SnapShot, Version) VALUES (@eventProviderId, @snapShot, @version);";

            using var sqliteConnection = new SqliteConnection(_sqLiteConnectionString);
            sqliteConnection.Open();

            using var sqliteTransaction = sqliteConnection.BeginTransaction();
            try
            {
                using var sqliteCommand = new SqliteCommand(commandText, sqliteTransaction.Connection, sqliteTransaction);
                sqliteCommand.Parameters.Add(new SqliteParameter("@eventProviderId", snapShot.EventProviderId));
                sqliteCommand.Parameters.Add(new SqliteParameter("@snapShot", Serialize(snapShot)));
                sqliteCommand.Parameters.Add(new SqliteParameter("@version", snapShot.Version));

                sqliteCommand.ExecuteNonQuery();
                sqliteTransaction.Commit();
            }
            catch (Exception)
            {
                sqliteTransaction.Rollback();
                throw;
            }
        }

        private static void UpdateEventProviderVersion(IEventProvider<TDomainEvent> eventProvider, SqliteTransaction transaction)
        {
            const string commandText = "UPDATE EventProviders SET Version = @version WHERE EventProviderId = @eventProviderId;";
            using var sqLiteCommand = new SqliteCommand(commandText, transaction.Connection, transaction);
            sqLiteCommand.Parameters.Add(new SqliteParameter("@eventProviderId", eventProvider.Id));
            sqLiteCommand.Parameters.Add(new SqliteParameter("@version", eventProvider.Version));

            sqLiteCommand.ExecuteNonQuery();
        }

        private static int GetEventProviderVersion(IEventProvider<TDomainEvent> eventProvider, SqliteTransaction transaction)
        {
            const string commandText = @"
                INSERT OR IGNORE INTO EventProviders VALUES (@eventProviderId, @type, 0);
                SELECT Version FROM EventProviders WHERE EventProviderId = @eventProviderId";
            using var sqLiteCommand = new SqliteCommand(commandText, transaction.Connection, transaction);
            sqLiteCommand.Parameters.Add(new SqliteParameter("@eventProviderId", eventProvider.Id));
            sqLiteCommand.Parameters.Add(new SqliteParameter("@type", eventProvider.GetType().FullName));
            sqLiteCommand.Parameters.Add(new SqliteParameter("@version", eventProvider.Version));

            return Convert.ToInt32(sqLiteCommand.ExecuteScalar());
        }

        private byte[] Serialize<T>(T theObject)
        {
            using var memoryStream = new MemoryStream();
            _formatter.Serialize(memoryStream, theObject);
            return memoryStream.ToArray();
        }

        private TType Deserialize<TType>(byte[] bytes)
        {
            using var memoryStream = new MemoryStream(bytes);
            return _formatter.Deserialize<TType>(memoryStream);
        }
    }
}