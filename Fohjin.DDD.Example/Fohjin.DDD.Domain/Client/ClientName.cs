using System.Text.Json.Serialization;

namespace Fohjin.DDD.Domain.Client
{
    public record ClientName
    {
        public string? Name { get; init; }

        [JsonConstructor]
        public ClientName() { }
        public ClientName(string? name)
        {
            Name = name;
        }
    }
}