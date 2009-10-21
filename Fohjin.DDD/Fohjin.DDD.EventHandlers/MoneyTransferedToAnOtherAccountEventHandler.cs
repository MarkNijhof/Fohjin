using System;
using Fohjin.DDD.Events.ActiveAccount;

namespace Fohjin.DDD.EventHandlers
{
    public class MoneyTransferedToAnOtherAccountEventHandler : IEventHandler<MoneyTransferedToAnOtherAccountEvent>
    {
        public void Execute(MoneyTransferedToAnOtherAccountEvent theEvent)
        {
            throw new NotImplementedException();
        }
    }
}