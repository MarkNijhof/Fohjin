using System.Collections.Generic;

namespace Fohjin.DDD.Bus
{
    public interface IBus : IUnitOfWork
    {
        void Publish(object message);
        void Publish(IEnumerable<object> messages);
    }
}