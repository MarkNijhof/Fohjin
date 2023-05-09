using Fohjin.DDD.Domain.Account;
using Fohjin.DDD.EventStore.Storage.Memento;

namespace Fohjin.DDD.Domain.Mementos
{
    [Serializable]
    public class ActiveAccountMemento : IMemento
    {
        internal Guid Id { get; init; }
        internal int Version { get; init; }
        internal Guid ClientId { get; init; }
        internal string AccountName { get; init; }
        internal string AccountNumber { get; set; }
        internal decimal Balance { get; init; }
        internal bool Closed { get; init; }
        internal List<KeyValuePair<string, string>> Ledgers { get; init; }

        public ActiveAccountMemento(Guid id, int version, Guid clientId, string accountName, string accountNumber, decimal balance, List<Ledger> ledgers, bool closed)
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