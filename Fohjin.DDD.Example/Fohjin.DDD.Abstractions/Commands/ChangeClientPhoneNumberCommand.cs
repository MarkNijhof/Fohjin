using System.Text.Json.Serialization;

namespace Fohjin.DDD.Commands
{
    public record ChangeClientPhoneNumberCommand : CommandBase
    {
        public string? PhoneNumber { get; init; }

        [JsonConstructor]
        public ChangeClientPhoneNumberCommand() : base() { }
        public ChangeClientPhoneNumberCommand(Guid id, string? phoneNumber) : base(id)
        {
            PhoneNumber = phoneNumber;
        }
    }
}