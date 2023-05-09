using Fohjin.DDD.EventStore.Storage.Memento;

namespace Fohjin.DDD.Domain.Mementos
{
    [Serializable]
    public class BankCardMemento : IMemento
    {
        internal Guid Id { get; init; }
        internal Guid AccountId { get; init; }
        internal bool Disabled { get; init; }

        public BankCardMemento(Guid id, Guid accountId, bool disabled)
        {
            Id = id;
            AccountId = accountId;
            Disabled = disabled;
        }
    }
}