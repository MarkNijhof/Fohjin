using Fohjin.DDD.Domain.Account;
using Fohjin.DDD.EventStore.Storage.Memento;

namespace Fohjin.DDD.Domain.Mementos
{
    [Serializable]
    public class ClosedAccountMemento : IMemento
    {
        internal Guid Id { get; init; }
        internal int Version { get; init; }
        internal Guid OriginalAccountId { get; init; }
        internal Guid ClientId { get; init; }
        internal string AccountName { get; init; }
        internal string AccountNumber { get; init; }
        internal List<KeyValuePair<string, string>> Ledgers { get; init; }

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