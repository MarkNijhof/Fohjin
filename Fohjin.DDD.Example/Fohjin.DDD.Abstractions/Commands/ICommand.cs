using Fohjin.DDD.EventStore.Storage;
using System.Text.Json;

namespace Fohjin.DDD.Commands
{
    [JsonInterfaceConverter(typeof(InterfaceConverter<ICommand>))]
    public interface ICommand
    {
        Guid Id { get; set; }
    }
}