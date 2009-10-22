using System;
using System.Collections.Generic;

namespace Fohjin.DDD.Domain.Entities.Mementos
{
    [Serializable]
    public class ClosedAccountMemento : IMemento
    {
        internal Guid Id { get; private set; }
        internal int Version { get; private set; }
        internal List<KeyValuePair<string, decimal>> Ledgers { get; private set; }

        public ClosedAccountMemento(Guid id, int version, List<Ledger> ledgers)
        {
            Id = id;
            Version = version;
            Ledgers = new List<KeyValuePair<string, decimal>>();
            ledgers.ForEach(x => Ledgers.Add(new KeyValuePair<string, decimal>(x.GetType().Name, x)));
        }
    }
}