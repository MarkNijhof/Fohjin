namespace Fohjin.DDD.Bus
{
    public interface IUnitOfWork
    {
        Task CommitAsync();
        void Rollback();
    }
}