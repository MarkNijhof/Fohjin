namespace Fohjin.DDD.EventStore
{
    public interface IRegisterChildEntities<TDomainEvent> where TDomainEvent : IDomainEvent
    {
        void RegisterChildEventProvider(IEntityEventProvider<TDomainEvent> entityEventProvider);
    }
}