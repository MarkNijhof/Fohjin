using System;
using System.Collections.Generic;
using System.Linq;
using Fohjin.DDD.Domain.Entities.Mementos;
using Fohjin.DDD.Domain.ValueObjects;
using Fohjin.DDD.Events.ClosedAccount;

namespace Fohjin.DDD.Domain.Entities
{
    public class ClosedAccount : BaseAggregateRoot, IOrginator
    {
        private readonly List<Ledger> _ledgers;

        public ClosedAccount()
        {
            _ledgers = new List<Ledger>();

            registerEvents();
        }

        private ClosedAccount(Guid accountId, Guid clientId, List<Ledger> ledgers, string accountName, string accountNumber) : this()
        {
            var Ledgers = new List<KeyValuePair<string, string>>();
            ledgers.ForEach(x => Ledgers.Add(new KeyValuePair<string, string>(x.GetType().Name, string.Format("{0}|{1}", ((decimal)x.Amount), x.Account.Number))));

            Apply(new ClosedAccountCreatedEvent(accountId, clientId, Ledgers, accountName, accountNumber));
        }

        public static ClosedAccount CreateNew(Guid accountId, Guid clientId, List<Ledger> ledgers, AccountName accountName, AccountNumber accountNumber)
        {
            return new ClosedAccount(accountId, clientId, ledgers, accountName.Name, accountNumber.Number);
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
                var split = ledger.Value.Split(new[] {'|'});
                var amount = new Amount(Convert.ToDecimal(split[0]));
                var account = new AccountNumber(split[1]);
                _ledgers.Add(InstantiateClassFromStringValue<Ledger>(ledger.Key, amount, account));
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

        private void registerEvents()
        {
            RegisterEvent<ClosedAccountCreatedEvent>(onClosedAccountCreated);
        }

        private void onClosedAccountCreated(ClosedAccountCreatedEvent closedAccountCreatedEvent)
        {
            Id = closedAccountCreatedEvent.ClientId;

            foreach (var ledger in closedAccountCreatedEvent.Ledgers)
            {
                var split = ledger.Value.Split(new[] { '|' });
                var amount = new Amount(Convert.ToDecimal(split[0]));
                var account = new AccountNumber(split[1]);
                _ledgers.Add(InstantiateClassFromStringValue<Ledger>(ledger.Key, amount, account));
            }
        }
    }
}