using System.Text.Json;

namespace Fohjin.DDD.Common
{
    public class ExtendedFormatter : IExtendedFormatter
    {
        public T Deserialize<T>(Stream stream) =>
            JsonSerializer.Deserialize<T>(stream);

        public void Serialize<T>(Stream stream, T graph) =>
            JsonSerializer.Serialize<T>(stream, graph);
    }
}