namespace Fohjin.DDD.EventStore
{
    public interface IEventProvider<TDomainEvent> where TDomainEvent : IDomainEvent
    {
        void Clear();
        void LoadFromHistory(IEnumerable<TDomainEvent> domainEvents);
        void UpdateVersion(int version);
        Guid Id { get; set; }
        int Version { get; }
        IEnumerable<TDomainEvent> GetChanges();
    }
}