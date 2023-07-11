using System.Text.Json.Serialization;

namespace Fohjin.DDD.Domain.Account
{
    public record AccountName
    {
        public string? Name { get; init; }

        [JsonConstructor]
        public AccountName() { }
        public AccountName(string? name)
        {
            Name = name;
        }

        public override string? ToString() => Name;
    }
}