using System.Text.Json.Serialization;

namespace Fohjin.DDD.Domain.Client
{
    public class PhoneNumber
    {
        public string Number { get; set; }

        [JsonConstructor]
        public PhoneNumber() { }

        public PhoneNumber(string number)
        {
            Number = number;
        }
    }
}