
namespace Fohjin.DDD.EventStore
{
    public interface IUnitOfWork
    {
        void Commit();
        void Rollback();
    }
}