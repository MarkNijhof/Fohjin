using Fohjin.DDD.EventStore.Storage.Memento;
using System.Text.Json;

namespace Fohjin.DDD.EventStore.Storage
{
    [JsonInterfaceConverter(typeof(InterfaceConverter<ISnapShot>))]
    public interface ISnapShot
    {
        IMemento Memento { get; }
        Guid EventProviderId { get; }
        int Version { get; }
    }
}