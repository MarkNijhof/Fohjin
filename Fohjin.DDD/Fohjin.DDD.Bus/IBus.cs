using System.Collections.Generic;

namespace Fohjin.DDD.Bus
{
    public interface ICommandBus : IBus {}
    public interface IEventBus : IBus {}

    public interface IBus
    {
        void Publish<TMessage>(TMessage message) where TMessage : class, IMessage;
        void Publish(IEnumerable<IMessage> messages);
    }
}