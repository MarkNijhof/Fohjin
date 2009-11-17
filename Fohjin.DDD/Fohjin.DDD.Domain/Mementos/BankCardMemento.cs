using System;
using Fohjin.DDD.EventStore.Storage.Memento;

namespace Fohjin.DDD.Domain.Mementos
{
    [Serializable]
    public class BankCardMemento : IMemento
    {
        internal Guid Id { get; private set; }
        internal Guid AccountId { get; private set; }
        internal bool Disabled { get; private set; }

        public BankCardMemento(Guid id, Guid accountId, bool disabled)
        {
            Id = id;
            AccountId = accountId;
            Disabled = disabled;
        }
    }
}