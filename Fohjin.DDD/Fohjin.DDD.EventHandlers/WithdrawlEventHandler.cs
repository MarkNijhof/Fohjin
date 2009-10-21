using System;
using Fohjin.DDD.Events.ActiveAccount;

namespace Fohjin.DDD.EventHandlers
{
    public class WithdrawlEventHandler : IEventHandler<WithdrawlEvent>
    {
        public void Execute(WithdrawlEvent theEvent)
        {
            throw new NotImplementedException();
        }
    }
}