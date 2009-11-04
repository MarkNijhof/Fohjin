namespace Fohjin.DDD.EventStore
{
    public interface IOrginator
    {
        IMemento CreateMemento();
        void SetMemento(IMemento memento);
    }
}