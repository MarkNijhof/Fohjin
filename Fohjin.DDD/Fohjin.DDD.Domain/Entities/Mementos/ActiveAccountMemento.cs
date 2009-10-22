using System;
using System.Collections.Generic;

namespace Fohjin.DDD.Domain.Entities.Mementos
{
    [Serializable]
    public class ActiveAccountMemento : IMemento
    {
        internal Guid Id { get; private set; }
        internal int Version { get; private set; }
        internal string AccountName { get; private set; }
        internal string AccountNumber { get; set; }
        internal decimal Balance { get; private set; }
        internal bool Closed { get; private set; }
        internal List<KeyValuePair<string, decimal>> Ledgers { get; private set; }

        public ActiveAccountMemento(Guid id, int version, string accountName, string accountNumber, decimal balance, List<Ledger> ledgers, bool closed)
        {
            Id = id;
            Version = version;
            AccountName = accountName;
            AccountNumber = accountNumber;
            Balance = balance;
            Closed = closed;
            Ledgers = new List<KeyValuePair<string, decimal>>();
            ledgers.ForEach(x => Ledgers.Add(new KeyValuePair<string, decimal>(x.GetType().Name, x)));
        }
    }
}