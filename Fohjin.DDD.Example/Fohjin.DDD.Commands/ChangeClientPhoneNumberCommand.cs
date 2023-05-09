namespace Fohjin.DDD.Commands
{
    [Serializable]
    public class ChangeClientPhoneNumberCommand : Command
    {
        public string PhoneNumber { get; init; }

        public ChangeClientPhoneNumberCommand(Guid id, string phoneNumber) : base(id)
        {
            PhoneNumber = phoneNumber;
        }
    }
}