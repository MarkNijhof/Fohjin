using System;
using Fohjin.DDD.Events.ActiveAccount;

namespace Fohjin.DDD.EventHandlers
{
    public class AccountNameGotChangedEventHandler : IEventHandler<AccountNameGotChangedEvent>
    {
        public void Execute(AccountNameGotChangedEvent command)
        {
            throw new NotImplementedException();
        }
    }
}