using System;
using Fohjin.DDD.Events.ActiveAccount;

namespace Fohjin.DDD.EventHandlers
{
    public class AccountCreatedEventHandler : IEventHandler<AccountCreatedEvent>
    {
        public void Execute(AccountCreatedEvent command)
        {
            throw new NotImplementedException();
        }
    }
}