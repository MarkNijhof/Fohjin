using System.Text.Json;

namespace Fohjin.DDD.EventStore.Storage.Memento
{
    [JsonInterfaceConverter(typeof(InterfaceConverter<IMemento>))]
    public interface IMemento
    {
    }
}