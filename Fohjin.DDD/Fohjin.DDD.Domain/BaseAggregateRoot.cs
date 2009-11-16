using System.Collections.Generic;
using System.Linq;
using Fohjin.DDD.EventStore;

namespace Fohjin.DDD.Domain
{
    public class BaseAggregateRoot : BaseEntity, IAggregateRootEventProvider
    {
        private readonly List<IEventProvider> _childEventProviders;

        public BaseAggregateRoot()
        {
            _childEventProviders = new List<IEventProvider>();
        }

        void IAggregateRootEventProvider.RegisterChildEventProvider(IEventProvider eventProvider)
        {
            _childEventProviders.Add(eventProvider);
        }

        IEnumerable<IDomainEvent> IEventProvider.GetChanges()
        {
            return GetChanges().Concat(_childEventProviders.SelectMany(x => x.GetChanges()));
        }

        void IEventProvider.Clear()
        {
            _childEventProviders.ForEach(x => x.Clear());
            Clear();
        }
    }
}