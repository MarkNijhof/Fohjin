using System;
using System.Collections.Generic;
using System.Data.SQLite;
using Fohjin.DDD.Domain.Events;

namespace Fohjin.DDD.Domain.Repositories
{
    public class DomainEventStorage : IDomainEventStorage
    {
        private readonly SQLiteConnection _sqLiteConnection;

        public DomainEventStorage(SQLiteConnection sqLiteConnection)
        {
            _sqLiteConnection = sqLiteConnection;
        }

        public IEnumerable<IDomainEvent> GetEventsAfter(int index)
        {
            throw new NotImplementedException();
        }

        public void AddEvent(Guid id, IDomainEvent domainEvent)
        {
            throw new NotImplementedException();
        }
    }
}