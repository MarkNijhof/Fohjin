
namespace Fohjin.DDD.EventStore
{
    public interface IUnitOfWork
    {
        void Complete();
        void Commit();
        void Rollback();
    }
}