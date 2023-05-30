namespace Fohjin.DDD.EventStore.Aggregate
{
    public abstract class BaseEntity<TDomainEvent> : IEntityEventProvider<TDomainEvent> where TDomainEvent : IDomainEvent
    {
        public Guid Id { get; set; }
        private readonly Dictionary<Type, Action<TDomainEvent>> _events = new();
        private readonly List<TDomainEvent> _appliedEvents = new();
        private Func<int>? _versionProvider;

        protected void RegisterEvent<TEvent>(Action<TEvent> eventHandler) where TEvent : class, TDomainEvent
        {
            _events.Add(typeof(TEvent), theEvent => eventHandler(theEvent as TEvent));
        }

        protected void Apply<TEvent>(TEvent domainEvent) where TEvent : class, TDomainEvent
        {
            domainEvent.AggregateId = Id;
            domainEvent.Version = _versionProvider?.Invoke() ?? -1;
            Apply(domainEvent.GetType(), domainEvent);
            _appliedEvents.Add(domainEvent);
        }

        void IEntityEventProvider<TDomainEvent>.LoadFromHistory(IEnumerable<TDomainEvent> domainEvents)
        {
            foreach (var domainEvent in domainEvents)
            {
                Apply(domainEvent.GetType(), domainEvent);
            }
        }

        public void HookUpVersionProvider(Func<int> versionProvider) => _versionProvider = versionProvider;
        IEnumerable<TDomainEvent> IEntityEventProvider<TDomainEvent>.GetChanges() => _appliedEvents;
        void IEntityEventProvider<TDomainEvent>.Clear() => _appliedEvents.Clear();

        private void Apply(Type eventType, TDomainEvent domainEvent)
        {
            if (!_events.TryGetValue(eventType, out Action<TDomainEvent> handler))
                throw new UnregisteredDomainEventException(string.Format("The requested domain event '{0}' is not registered in '{1}'", eventType.FullName, GetType().FullName));

            handler(domainEvent);
        }
    }
}