using System;
using System.Collections.Generic;
using Fohjin.DDD.Domain.Entities.ActiveAccountStates;
using Fohjin.DDD.Domain.ValueObjects;

namespace Fohjin.DDD.Domain.Entities.Mementos
{
    [Serializable]
    public class ActiveAccountMemento : IMemento
    {
        internal string State;
        internal List<KeyValuePair<string, decimal>> Mutations { get; private set; }
        internal decimal Balance { get; private set; }
        internal int Version { get; private set; }
        internal Guid Id { get; private set; }
        internal string AccountName { get; private set; }

        public ActiveAccountMemento(Guid id, int version, string accountName, Balance balance, List<Ledger> mutations, IActiveAccountState state)
        {
            Id = id;
            Version = version;
            AccountName = accountName;
            Balance = balance;
            Mutations = new List<KeyValuePair<string, decimal>>();
            mutations.ForEach(x => Mutations.Add(new KeyValuePair<string, decimal>(x.GetType().Name, x)));
            State = state.GetType().Name;
        }
    }
}