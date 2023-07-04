using System.Text.Json.Serialization;

namespace Fohjin.DDD.Events.Client
{
    public record ClientPhoneNumberChangedEvent : DomainEvent
    {
        public string? PhoneNumber { get; set; } 

        [JsonConstructor]
        public ClientPhoneNumberChangedEvent() { }
        public ClientPhoneNumberChangedEvent(string? phoneNumber)
        {
            PhoneNumber = phoneNumber;
        }
    }
}