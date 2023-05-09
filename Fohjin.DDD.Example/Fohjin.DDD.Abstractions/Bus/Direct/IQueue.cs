namespace Fohjin.DDD.Bus.Direct
{
    public interface IQueue
    {
        void Put(object item);
        Task PopAsync(Func<object, Task> popAction);
    }
}