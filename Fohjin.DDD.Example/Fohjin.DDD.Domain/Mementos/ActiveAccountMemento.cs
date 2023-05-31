using Fohjin.DDD.Domain.Account;
using Fohjin.DDD.EventStore.Storage.Memento;
using System.Text.Json.Serialization;

namespace Fohjin.DDD.Domain.Mementos
{
    public class ActiveAccountMemento : IMemento
    {
        public Guid Id { get; set; }
        public int Version { get; set; }
        public Guid ClientId { get; set; }
        public string AccountName { get; set; }
        public string AccountNumber { get; set; }
        public decimal Balance { get; set; }
        public bool Closed { get; set; }
        public List<KeyValuePair<string, string>> Ledgers { get; set; }

        [JsonConstructor]
        public ActiveAccountMemento() { }

        public ActiveAccountMemento(
            Guid id,
            int version, 
            Guid clientId, 
            string accountName, 
            string accountNumber,
            decimal balance,
            List<Ledger> ledgers,
            bool closed
            )
        {
            Id = id;
            Version = version;
            ClientId = clientId;
            AccountName = accountName;
            AccountNumber = accountNumber;
            Balance = balance;
            Closed = closed;
            Ledgers = new List<KeyValuePair<string, string>>();
            ledgers.ForEach(x => Ledgers.Add(new KeyValuePair<string, string>(x.GetType().Name, string.Format("{0}|{1}", ((decimal)x.Amount), x.Account.Number))));
        }
    }
}