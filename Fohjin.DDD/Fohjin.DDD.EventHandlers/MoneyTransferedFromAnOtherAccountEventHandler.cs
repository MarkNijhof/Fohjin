using System;
using Fohjin.DDD.Events.ActiveAccount;

namespace Fohjin.DDD.EventHandlers
{
    public class MoneyTransferedFromAnOtherAccountEventHandler : IEventHandler<MoneyTransferedFromAnOtherAccountEvent>
    {
        public void Execute(MoneyTransferedFromAnOtherAccountEvent theEvent)
        {
            throw new NotImplementedException();
        }
    }
}