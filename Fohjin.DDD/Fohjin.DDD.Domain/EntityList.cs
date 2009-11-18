using System.Collections.Generic;
using Fohjin.DDD.EventStore;

namespace Fohjin.DDD.Domain
{
    public class EntityList<TEntity> : List<TEntity> where TEntity : IEntityEventProvider
    {
        private readonly IRegisterChildEntities _aggregateRoot;

        public EntityList(IRegisterChildEntities aggregateRoot)
        {
            _aggregateRoot = aggregateRoot;
        }

        public EntityList(IRegisterChildEntities aggregateRoot, int capacity) : base(capacity)
        {
            _aggregateRoot = aggregateRoot;
        }

        public EntityList(IRegisterChildEntities aggregateRoot, IEnumerable<TEntity> collection) : base(collection)
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