namespace Fohjin.DDD.Bus.Direct
{
    public interface IQueue
    {
        void Put(object item);
        void Pop(Action<object> popAction);
    }
}