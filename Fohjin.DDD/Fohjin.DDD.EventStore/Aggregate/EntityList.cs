using System.Collections.Generic;

namespace Fohjin.DDD.EventStore.Aggregate
{
    public class EntityList<TEntity, TDomainEvent> : List<TEntity>
        where TEntity : IEntityEventProvider<TDomainEvent>
        where TDomainEvent : IDomainEvent
    {
        private readonly IRegisterChildEntities<TDomainEvent> _aggregateRoot;

        public EntityList(IRegisterChildEntities<TDomainEvent> aggregateRoot)
        {
            _aggregateRoot = aggregateRoot;
        }

        public EntityList(IRegisterChildEntities<TDomainEvent> aggregateRoot, int capacity)
            : base(capacity)
        {
            _aggregateRoot = aggregateRoot;
        }

        public EntityList(IRegisterChildEntities<TDomainEvent> aggregateRoot, IEnumerable<TEntity> collection)
            : base(collection)
        {
            _aggregateRoot = aggregateRoot;
        }

        public new void Add(TEntity entity)
        {
            _aggregateRoot.RegisterChildEventProvider(entity);
            base.Add(entity);
        }
    }
}