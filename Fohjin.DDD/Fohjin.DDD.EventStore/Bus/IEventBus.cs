using System.Collections.Generic;

namespace Fohjin.DDD.EventStore.Bus
{
    public interface IEventBus
    {
        void PublishMultiple<TMessage>(IEnumerable<TMessage> messages) where TMessage : class;
    }
}