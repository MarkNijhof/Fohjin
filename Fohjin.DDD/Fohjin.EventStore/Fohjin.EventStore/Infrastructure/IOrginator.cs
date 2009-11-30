using System;

namespace Fohjin.EventStore.Infrastructure
{
    public interface IOrginator
    {
        IMemento CreateMemento();
        void SetMemento(IMemento memento);
    }

    public class Orginator : IOrginator
    {
        public IMemento CreateMemento()
        {
            throw new NotImplementedException();
        }

        public void SetMemento(IMemento memento)
        {
            throw new NotImplementedException();
        }
    }
}