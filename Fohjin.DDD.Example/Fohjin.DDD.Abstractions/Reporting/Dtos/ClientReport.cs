using System.Text.Json.Serialization;

namespace Fohjin.DDD.Reporting.Dtos
{
    public class ClientReport
    {
        public Guid Id { get; set; }
        public string? Name { get; set; }

        [JsonConstructor]
        public ClientReport() { }


        [SqliteConstructor]
        public ClientReport(Guid id, string? name)
        {
            Id = id;
            Name = name;
        }

        public override string? ToString() => Name;
    }
}