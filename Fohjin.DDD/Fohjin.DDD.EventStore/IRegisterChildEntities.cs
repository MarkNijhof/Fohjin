namespace Fohjin.DDD.EventStore
{
    public interface IRegisterChildEntities
    {
        void RegisterChildEventProvider(IEntityEventProvider entityEventProvider);
    }
}