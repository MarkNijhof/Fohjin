using System.Collections.Generic;
using Fohjin.DDD.EventHandlers;
using StructureMap;

namespace Fohjin.DDD.Bus.Implementation
{
    public class DirectEventBus : IEventBus
    {
        private readonly IContainer _container;

        public DirectEventBus(IContainer container)
        {
            _container = container;
        }

        public void Publish<TMessage>(TMessage message) where TMessage : class, IMessage
        {
            var handler = _container.GetInstance<IEventHandler<TMessage>>();
            handler.Execute(message);
        }

        public void Publish(IEnumerable<IMessage> messages)
        {
            foreach (var message in messages)
            {
                Publish(message);
            }
        }
    }
}