namespace Fohjin.DDD.Domain.Mementos
{
    public interface IOrginator 
    {
        IMemento CreateMemento();
        void SetMemento(IMemento memento);
    }
}