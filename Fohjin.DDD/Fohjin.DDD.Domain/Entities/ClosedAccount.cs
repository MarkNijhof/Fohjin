using System;
using System.Collections.Generic;
using System.Linq;
using Fohjin.DDD.Domain.Entities.Mementos;
using Fohjin.DDD.Domain.ValueObjects;

namespace Fohjin.DDD.Domain.Entities
{
    public class ClosedAccount : BaseAggregateRoot, IOrginator
    {
        private readonly List<Ledger> _ledgers;

        public ClosedAccount()
        {
            _ledgers = new List<Ledger>();
        }

        public ClosedAccount(Guid id, List<Ledger> mutations)
        {
            Id = id;
            _ledgers = mutations;
        }

        public IMemento CreateMemento()
        {
            return new ClosedAccountMemento(Id, Version, _ledgers);
        }

        public void SetMemento(IMemento memento)
        {
            var closedAccountMemento = (ClosedAccountMemento)memento;
            Id = closedAccountMemento.Id;
            Version = closedAccountMemento.Version;

            foreach (var ledger in closedAccountMemento.Ledgers)
            {
                _ledgers.Add(InstantiateClassFromStringValue<Ledger>(ledger.Key, new Amount(ledger.Value)));
            }
        }

        private TRequestedType InstantiateClassFromStringValue<TRequestedType>(string className, params object[] constructorArguments)
        {
            var classType = GetType()
                .Assembly
                .GetExportedTypes()
                .Where(x => x.Name == className)
                .FirstOrDefault();

            return (TRequestedType)Activator.CreateInstance(classType, constructorArguments);
        }
    }
}