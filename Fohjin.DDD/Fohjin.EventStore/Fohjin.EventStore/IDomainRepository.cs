using Fohjin.EventStore.Infrastructure;

namespace Fohjin.EventStore
{
    public interface IDomainRepository
    {
        TAggregateRoot CreateNew<TAggregateRoot>() where TAggregateRoot : class;
        
        //TAggregate GetById<TAggregate>(Guid id)
        //    where TAggregate : class, IOrginator, IEventProvider<TDomainEvent>, new();

        //void Add<TAggregate>(TAggregate aggregateRoot)
        //    where TAggregate : class, IOrginator, IEventProvider<TDomainEvent>, new();
    }

    public class DomainRepository : IDomainRepository
    {
        private readonly IAggregateRootFactory _aggregateRootFactory;

        public DomainRepository(IAggregateRootFactory aggregateRootFactory)
        {
            _aggregateRootFactory = aggregateRootFactory;
        }

        public TAggregateRoot CreateNew<TAggregateRoot>() where TAggregateRoot : class
        {
            return _aggregateRootFactory.Create<TAggregateRoot>();
        }
    }
}