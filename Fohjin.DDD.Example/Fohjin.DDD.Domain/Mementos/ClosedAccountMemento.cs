using Fohjin.DDD.Domain.Account;
using Fohjin.DDD.EventStore.Storage.Memento;
using System.Text.Json.Serialization;

namespace Fohjin.DDD.Domain.Mementos
{
    public record ClosedAccountMemento : IMemento
    {
        public Guid Id { get; init; }
        public int Version { get; init; }
        public Guid OriginalAccountId { get; init; }
        public Guid ClientId { get; init; }
        public string? AccountName { get; init; }
        public string? AccountNumber { get; init; }
        public List<KeyValuePair<string, string>> Ledgers { get; init; } = new();

        [JsonConstructor]
        public ClosedAccountMemento() { }
        public ClosedAccountMemento(
            Guid id,
            int version,
            Guid originalAccountId,
            Guid clientId,
            string? accountName,
            string? accountNumber,
            List<Ledger> ledgers
            )
        {
            Id = id;
            Version = version;
            OriginalAccountId = originalAccountId;
            ClientId = clientId;
            AccountName = accountName;
            AccountNumber = accountNumber;
            Ledgers = new List<KeyValuePair<string, string>>();
            ledgers.ForEach(x => Ledgers.Add(new KeyValuePair<string, string>(x.GetType().Name, string.Format("{0}|{1}", ((decimal)x.Amount), x.Account.Number))));
        }
    }
}