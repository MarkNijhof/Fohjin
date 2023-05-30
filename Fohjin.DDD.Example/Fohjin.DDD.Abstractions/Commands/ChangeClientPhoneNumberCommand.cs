using System.Text.Json.Serialization;

namespace Fohjin.DDD.Commands
{
    public class ChangeClientPhoneNumberCommand : CommandBase
    {
        public string PhoneNumber { get; set; }

        [JsonConstructor]
        public ChangeClientPhoneNumberCommand() : base() { }
        public ChangeClientPhoneNumberCommand(Guid id, string phoneNumber) : base(id)
        {
            PhoneNumber = phoneNumber;
        }
    }
}