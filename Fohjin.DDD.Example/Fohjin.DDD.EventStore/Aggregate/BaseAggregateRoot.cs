namespace Fohjin.DDD.EventStore.Aggregate
{
    public class BaseAggregateRoot<TDomainEvent> : IEventProvider<TDomainEvent>, IRegisterChildEntities<TDomainEvent> where TDomainEvent : IDomainEvent
    {
        private readonly Dictionary<Type, Action<TDomainEvent>> _registeredEvents = new();
        private readonly List<TDomainEvent> _appliedEvents = new();
        private readonly List<IEntityEventProvider<TDomainEvent>> _childEventProviders = new();

        public Guid Id { get; set; }
        public int Version { get; protected set; }
        public int EventVersion { get; protected set; }

        public BaseAggregateRoot()
        {
        }

        protected void RegisterEvent<TEvent>(Action<TEvent> eventHandler) where TEvent : class, TDomainEvent
        {
            _registeredEvents.Add(typeof(TEvent), theEvent => eventHandler((TEvent)theEvent));
        }

        protected void Apply<TEvent>(TEvent domainEvent) where TEvent : class, TDomainEvent
        {
            domainEvent.AggregateId = Id;
            domainEvent.Version = GetNewEventVersion();
            Apply(domainEvent.GetType(), domainEvent);
            _appliedEvents.Add(domainEvent);
        }

        void IEventProvider<TDomainEvent>.LoadFromHistory(IEnumerable<TDomainEvent> domainEvents)
        {
            if (!domainEvents.Any())
                return;

            foreach (var domainEvent in domainEvents)
            {
                Apply(domainEvent.GetType(), domainEvent);
            }

            Version = domainEvents.Last().Version;
            EventVersion = Version;
        }

        private void Apply(Type eventType, TDomainEvent domainEvent)
        {

            if (!_registeredEvents.TryGetValue(eventType, out var handler))
                throw new UnregisteredDomainEventException($"The requested domain event '{eventType.FullName}' is not registered in '{GetType().FullName}'");

            handler(domainEvent);
        }

        IEnumerable<TDomainEvent> IEventProvider<TDomainEvent>.GetChanges()
        {
            return _appliedEvents.Concat(GetChildEventsAndUpdateEventVersion()).OrderBy(x => x.Version).ToList();
        }

        void IEventProvider<TDomainEvent>.Clear()
        {
            foreach (var item in _childEventProviders)
                item.Clear();
            _appliedEvents.Clear();
        }

        void IEventProvider<TDomainEvent>.UpdateVersion(int version)
        {
            Version = version;
        }

        void IRegisterChildEntities<TDomainEvent>.RegisterChildEventProvider(IEntityEventProvider<TDomainEvent> entityEventProvider)
        {
            entityEventProvider.HookUpVersionProvider(GetNewEventVersion);
            _childEventProviders.Add(entityEventProvider);
        }

        private IEnumerable<TDomainEvent> GetChildEventsAndUpdateEventVersion() =>
            _childEventProviders.SelectMany(entity => entity.GetChanges());

        private int GetNewEventVersion() => EventVersion++;
    }
}