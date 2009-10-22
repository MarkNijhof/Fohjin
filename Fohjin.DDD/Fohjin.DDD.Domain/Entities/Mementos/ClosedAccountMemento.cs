using System;
using System.Collections.Generic;

namespace Fohjin.DDD.Domain.Entities.Mementos
{
    [Serializable]
    public class ClosedAccountMemento : IMemento
    {
        internal Guid Id { get; private set; }
        internal int Version { get; private set; }
        internal List<KeyValuePair<string, string>> Ledgers { get; private set; }

        public ClosedAccountMemento(Guid id, int version, List<Ledger> ledgers)
        {
            Id = id;
            Version = version;
            Ledgers = new List<KeyValuePair<string, string>>();
            ledgers.ForEach(x => Ledgers.Add(new KeyValuePair<string, string>(x.GetType().Name, string.Format("{0}|{1}", ((decimal)x.Amount), x.Account.Number))));
        }
    }
}