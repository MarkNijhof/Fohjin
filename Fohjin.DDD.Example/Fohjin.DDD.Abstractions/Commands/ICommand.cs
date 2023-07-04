using System.Text.Json;

namespace Fohjin.DDD.Commands
{
    [JsonInterfaceConverter(typeof(InterfaceConverter<ICommand>))]
    public interface ICommand
    {
        Guid Id { get; init; }
    }
}