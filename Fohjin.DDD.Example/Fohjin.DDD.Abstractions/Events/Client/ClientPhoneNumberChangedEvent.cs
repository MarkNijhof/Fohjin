namespace Fohjin.DDD.Events.Client
{
    public class ClientPhoneNumberChangedEvent : DomainEvent
    {
        public string PhoneNumber { get; set; }

        public ClientPhoneNumberChangedEvent(string phoneNumber)
        {
            PhoneNumber = phoneNumber;
        }
    }
}