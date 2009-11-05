using System.Collections.Generic;

namespace Fohjin.DDD.EventStore.Bus
{
    public interface IEventBus
    {
        void Publish<TMessage>(TMessage message) where TMessage : class;
        void PublishMultiple<TMessage>(IEnumerable<TMessage> messages) where TMessage : class;
    }
}