namespace Fohjin.DDD.Bus.Direct
{
    public interface IQueue
    {
        Task PutAsync(object item);
        Task PopAsync(Func<object, Task> popAction);
    }
}