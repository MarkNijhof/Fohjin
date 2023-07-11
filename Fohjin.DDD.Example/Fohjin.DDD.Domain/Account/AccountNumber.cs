using System.Text.Json.Serialization;

namespace Fohjin.DDD.Domain.Account
{
    public class AccountNumber
    {
        public string? Number { get; set; } 

        [JsonConstructor]
        public AccountNumber() { }

        public AccountNumber(string? number)
        {
            Number = number;
        }

        public override string? ToString() => Number;
    }
}