using System.Collections.Generic;
using StructureMap;

namespace Fohjin.DDD.Bus.Implementation
{
    public class DirectCommandBus : ICommandBus
    {
        private readonly IContainer _container;

        public DirectCommandBus(IContainer container)
        {
            _container = container;
        }

        public void Publish<TMessage>(TMessage message) where TMessage : IMessage
        {
            var handler = _container.GetInstance<IHandler<TMessage>>();
            handler.Handle(message);
        }

        public void Publish<TMessage>(IEnumerable<TMessage> messages) where TMessage : IMessage
        {
            foreach (var message in messages)
            {
                Publish(message);
            }
        }
    }
}