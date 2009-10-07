using System.Collections.Generic;

namespace Fohjin.DDD.Bus
{
    public interface IHandler<TMessage> where TMessage : IMessage
    {
        void Handle(TMessage message);
        void Handle(IEnumerable<TMessage> messages);
    }
}