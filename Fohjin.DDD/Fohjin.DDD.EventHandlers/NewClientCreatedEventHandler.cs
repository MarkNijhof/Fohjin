using System;
using Fohjin.DDD.Events.Client;

namespace Fohjin.DDD.EventHandlers
{
    public class NewClientCreatedEventHandler : IEventHandler<NewClientCreatedEvent>
    {
        public void Execute(NewClientCreatedEvent command)
        {
            throw new NotImplementedException();
        }
    }
}