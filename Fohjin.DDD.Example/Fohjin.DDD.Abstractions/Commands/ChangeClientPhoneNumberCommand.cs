namespace Fohjin.DDD.Commands
{
    public class ChangeClientPhoneNumberCommand : Command
    {
        public string PhoneNumber { get; set; }

        public ChangeClientPhoneNumberCommand(Guid id, string phoneNumber) : base(id)
        {
            PhoneNumber = phoneNumber;
        }
    }
}