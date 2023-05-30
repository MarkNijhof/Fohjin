using System.Text.Json;

namespace Fohjin.DDD.EventStore.Storage.Memento
{
    public interface IOriginator
    {
        IMemento CreateMemento();
        void SetMemento(IMemento memento);
    }
}