using System;

namespace Fohjin.DDD.Commands
{
    [Serializable]
    public class ChangeClientPhoneNumberCommand : Command
    {
        public string PhoneNumber { get; private set; }

        public ChangeClientPhoneNumberCommand(Guid id, string phoneNumber) : base(id)
        {
            PhoneNumber = phoneNumber;
        }
    }
}