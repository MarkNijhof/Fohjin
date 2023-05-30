using System.Text.Json.Serialization;

namespace Fohjin.DDD.Events.Client
{
    public class ClientPhoneNumberChangedEvent : DomainEvent
    {
        public string PhoneNumber { get; set; } = null!;

        [JsonConstructor]
        public ClientPhoneNumberChangedEvent() { }
        public ClientPhoneNumberChangedEvent(string phoneNumber)
        {
            PhoneNumber = phoneNumber;
        }
    }
}