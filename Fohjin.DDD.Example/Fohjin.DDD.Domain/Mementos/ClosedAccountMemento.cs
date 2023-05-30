using Fohjin.DDD.Domain.Account;
using Fohjin.DDD.EventStore.Storage.Memento;
using System.Text.Json.Serialization;

namespace Fohjin.DDD.Domain.Mementos
{
    [Serializable]
    public class ClosedAccountMemento : IMemento
    {
        public Guid Id { get; set; }
        public int Version { get; set; }
        public Guid OriginalAccountId { get; set; }
        public Guid ClientId { get; set; }
        public string AccountName { get; set; }
        public string AccountNumber { get; set; }
        public List<KeyValuePair<string, string>> Ledgers { get; set; }

        [JsonConstructor]
        public ClosedAccountMemento() { }
        public ClosedAccountMemento(Guid id, int version, Guid originalAccountId, Guid clientId, string accountName, string accountNumber, List<Ledger> ledgers)
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