using System.Collections.Generic;
using Fohjin.DDD.EventStore;

namespace Fohjin.DDD.Domain
{
    public class EntityList<TEntity> : List<TEntity> where TEntity : BaseEntity
    {
        private readonly IEventProvider _aggregateRoot;

        public EntityList(IEventProvider aggregateRoot)
        {
            _aggregateRoot = aggregateRoot;
        }

        public EntityList(IEventProvider aggregateRoot, int capacity) : base(capacity)
        {
            _aggregateRoot = aggregateRoot;
        }

        public EntityList(IEventProvider aggregateRoot, IEnumerable<TEntity> collection) : base(collection)
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