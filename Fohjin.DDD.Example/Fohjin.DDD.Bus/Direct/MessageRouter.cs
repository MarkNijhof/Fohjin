namespace Fohjin.DDD.Bus.Direct
{
    public class MessageRouter : IRouteMessages
    {
        private readonly Dictionary<Type, ICollection<Action<object>>> _routes = new();

        public MessageRouter()
        {
        }

        public void Register<TMessage>(Action<TMessage> route) where TMessage : class
        {
            var routingKey = typeof(TMessage);
            ICollection<Action<object>> routes;

            if (!_routes.TryGetValue(routingKey, out routes))
                _routes[routingKey] = routes = new LinkedList<Action<object>>();

            routes.Add(message => route(message as TMessage));
        }

        public void Route(object message)
        {
            ICollection<Action<object>> routes;

            if (!_routes.TryGetValue(message.GetType(), out routes))
                throw new RouteNotRegisteredException(message.GetType());

            foreach (var route in routes)
                route(message);
        }
    }
}