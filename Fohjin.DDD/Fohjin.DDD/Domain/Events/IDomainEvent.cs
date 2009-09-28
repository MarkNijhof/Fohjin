
namespace Fohjin.DDD.Domain.Events
{
    public interface IDomainEvent
    {
        long TimeStamp { get; }
    }
}