namespace Fohjin.DDD.Bus
{
    public interface IUnitOfWork
    {
        void Commit();
        void Rollback();
    }
}