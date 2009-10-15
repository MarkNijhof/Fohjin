using System.Collections.Generic;
using Fohjin.DDD.CommandHandlers;
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

        public void Publish<TMessage>(TMessage message) where TMessage : class, IMessage
        {
            var handler = _container.GetInstance<ICommandHandler<TMessage>>();
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