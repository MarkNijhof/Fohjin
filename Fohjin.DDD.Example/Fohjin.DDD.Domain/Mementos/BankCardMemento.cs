using Fohjin.DDD.EventStore.Storage.Memento;
using System.Text.Json.Serialization;

namespace Fohjin.DDD.Domain.Mementos
{
    [Serializable]
    public class BankCardMemento : IMemento
    {
        public Guid Id { get; set; }
        public Guid AccountId { get; set; }
        public bool Disabled { get; set; }

        [JsonConstructor]
        public BankCardMemento() { }
        public BankCardMemento(Guid id, Guid accountId, bool disabled)
        {
            Id = id;
            AccountId = accountId;
            Disabled = disabled;
        }
    }
}