using System;
using Fohjin.DDD.Events.ActiveAccount;

namespace Fohjin.DDD.EventHandlers
{
    public class DepositeEventHandler : IEventHandler<DepositeEvent>
    {
        public void Execute(DepositeEvent theEvent)
        {
            throw new NotImplementedException();
        }
    }
}