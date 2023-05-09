namespace Fohjin.DDD.Events.Client
{
    public record ClientPhoneNumberChangedEvent : DomainEvent
    {
        public string PhoneNumber { get; init; }

        public ClientPhoneNumberChangedEvent(string phoneNumber)
        {
            PhoneNumber = phoneNumber;
        }
    }
}