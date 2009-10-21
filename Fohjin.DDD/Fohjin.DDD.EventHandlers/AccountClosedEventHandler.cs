using System;
using Fohjin.DDD.Events.ActiveAccount;

namespace Fohjin.DDD.EventHandlers
{
    public class AccountClosedEventHandler : IEventHandler<AccountClosedEvent>
    {
        public void Execute(AccountClosedEvent command)
        {
            throw new NotImplementedException();
        }
    }
}