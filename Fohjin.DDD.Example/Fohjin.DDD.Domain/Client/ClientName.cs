using System.Text.Json.Serialization;

namespace Fohjin.DDD.Domain.Client
{
    public class ClientName
    {
        public string Name { get; set; }

        [JsonConstructor]
        public ClientName() { }
        public ClientName(string name)
        {
            Name = name;
        }
    }
}