using System;
using Fohjin.DDD.EventStore.Storage.Memento;

namespace Fohjin.DDD.Domain.Mementos
{
    [Serializable]
    public class BankCardMemento : IMemento
    {
        internal Guid Id { get; private set; }
        internal int Version { get; private set; }
        internal Guid AccountId { get; private set; }
        internal bool Disabled { get; private set; }

        public BankCardMemento(Guid id, int version, Guid accountId, bool disabled)
        {
            Id = id;
            Version = version;
            AccountId = accountId;
            Disabled = disabled;
        }
    }
}