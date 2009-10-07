namespace Fohjin.DDD.Domain.Entities.Mementos
{
    public interface IOrginator 
    {
        IMemento CreateMemento();
        void SetMemento(IMemento memento);
    }
}