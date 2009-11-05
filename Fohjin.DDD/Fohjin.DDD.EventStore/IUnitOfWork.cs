using System;

namespace Fohjin.DDD.EventStore
{
    public interface IUnitOfWork : IDisposable
    {
        void Complete();
        void Commit();
        void Rollback();
    }
}