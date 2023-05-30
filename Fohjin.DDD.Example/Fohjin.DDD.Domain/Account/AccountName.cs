using System.Text.Json.Serialization;

namespace Fohjin.DDD.Domain.Account
{
    public class AccountName
    {
        public string Name { get; set; }

        [JsonConstructor]
        public AccountName() { }
        public AccountName(string name)
        {
            Name = name;
        }

        public override string ToString() => Name;
    }
}