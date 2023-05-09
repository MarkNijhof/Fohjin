namespace Fohjin.DDD.Commands
{
    public record ChangeClientPhoneNumberCommand : Command
    {
        public string PhoneNumber { get; init; }

        public ChangeClientPhoneNumberCommand(Guid id, string phoneNumber) : base(id)
        {
            PhoneNumber = phoneNumber;
        }
    }
}