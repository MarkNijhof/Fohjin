using System.Text.Json;

namespace Fohjin.DDD.EventStore
{
    [JsonInterfaceConverter(typeof(InterfaceConverter<IDomainEvent>))]
    public interface IDomainEvent
    {
        Guid Id { get; set; }
        Guid AggregateId { get; set; }
        int Version { get; set; }
    }
}