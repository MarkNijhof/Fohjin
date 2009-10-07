namespace Fohjin.DDD.Domain.Memento
{
    public interface IOrginator 
    {
        IMemento CreateMemento();
        void SetMemento(IMemento memento);
    }
}