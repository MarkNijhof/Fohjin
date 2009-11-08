using System;

namespace Fohjin.DDD.Bus.Direct
{
    public class RouteNotRegisteredException : Exception
    {
        public RouteNotRegisteredException(Type messageType) : base(string.Format("No route specified for message '{0}'", messageType.FullName))
        {
        }
    }
}