using System;

namespace Fohjin.DDD.Commands.Client
{
    public class ClientPhoneNumberIsChangedCommand : Command
    {
        public string PhoneNumber { get; private set; }

        public ClientPhoneNumberIsChangedCommand(Guid id, string phoneNumber) : base(id)
        {
            PhoneNumber = phoneNumber;
        }
    }
}