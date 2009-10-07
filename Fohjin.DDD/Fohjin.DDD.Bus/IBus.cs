using System.Collections.Generic;

namespace Fohjin.DDD.Bus
{
    public interface ICommandBus : IBus {}
    public interface IEventBus : IBus {}

    public interface IBus
    {
        void Publish<TMessage>(TMessage message) where TMessage : IMessage;
        void Publish<TMessage>(IEnumerable<TMessage> messages) where TMessage : IMessage;
    }
}