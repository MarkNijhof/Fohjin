using System.Collections.Generic;

namespace Fohjin.DDD.EventStore
{
    public interface ICommandBus : IBus {}
    public interface IEventBus : IBus {}

    public interface IBus
    {
        void Publish<TMessage>(TMessage message) where TMessage : class;
        void PublishMultiple<TMessage>(IEnumerable<TMessage> messages) where TMessage : class;
    }
}