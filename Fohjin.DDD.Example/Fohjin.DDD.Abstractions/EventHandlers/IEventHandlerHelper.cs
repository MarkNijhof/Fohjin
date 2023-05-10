using Fohjin.DDD.EventStore;

namespace Fohjin.DDD.Configuration
{
    public interface IEventHandlerHelper
    {
        Task<bool> RouteAsync(IDomainEvent message);
    }
}