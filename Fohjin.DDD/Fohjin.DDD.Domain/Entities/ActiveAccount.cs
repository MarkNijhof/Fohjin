using System;
using System.Collections.Generic;
using System.Linq;
using Fohjin.DDD.Domain.Entities.Mementos;
using Fohjin.DDD.Domain.ValueObjects;
using Fohjin.DDD.Events.ActiveAccount;

namespace Fohjin.DDD.Domain.Entities
{
    public class ActiveAccount : BaseAggregateRoot, IOrginator
    {
        private AccountName _accountName;
        private Balance _balance;
        private readonly List<Ledger> _ledgers;
        private bool _closed;

        public ActiveAccount()
        {
            Id = new Guid();
            Version = 0;
            EventVersion = 0;
            _accountName = new AccountName(string.Empty);
            _balance = new Balance();
            _ledgers = new List<Ledger>();
            _closed = false;

            registerEvents();
        }

        public ActiveAccount(string accountName) : this()
        {
            Apply(new AccountCreatedEvent(Guid.NewGuid(), accountName));
        }

        public ClosedAccount Close()
        {
            if (Id == new Guid())
                throw new Exception("The ActiveAcount is not created and no opperations can be executed on it");

            if (_closed)
                throw new Exception("The ActiveAcount is closed and no opperations can be executed on it");

            var closedAccount = new ClosedAccount(Id, _ledgers);
            Apply(new AccountClosedEvent());
            return closedAccount;
        }

        public void Withdrawl(Amount amount)
        {
            if (Id == new Guid())
                throw new Exception("The ActiveAcount is not created and no opperations can be executed on it");

            if (_closed)
                throw new Exception("The ActiveAcount is closed and no opperations can be executed on it");

            if (_balance.WithdrawlWillResultInNegativeBalance(amount))
                throw new Exception(string.Format("The amount {0} is larger than your current balance {1}", (decimal)amount, (decimal)_balance));

            var newBalance = _balance.Withdrawl(amount);

            Apply(new WithdrawlEvent(newBalance, amount));
        }

        public void Deposite(Amount amount)
        {
            if (Id == new Guid())
                throw new Exception("The ActiveAcount is not created and no opperations can be executed on it");

            if (_closed)
                throw new Exception("The ActiveAcount is closed and no opperations can be executed on it");

            var newBalance = _balance.Deposite(amount);

            Apply(new DepositeEvent(newBalance, amount));
        }

        public static ActiveAccount CreateNew(string accountName)
        {
            return new ActiveAccount(accountName);
        }

        IMemento IOrginator.CreateMemento()
        {
            return new ActiveAccountMemento(Id, Version, _accountName.Name, _balance, _ledgers, _closed);
        }

        void IOrginator.SetMemento(IMemento memento)
        {
            var accountMemento = (ActiveAccountMemento) memento;
            Id = accountMemento.Id;
            Version = accountMemento.Version;
            _closed = accountMemento.Closed;
            _balance = accountMemento.Balance;
            _accountName = new AccountName(accountMemento.AccountName);

            foreach (var mutation in accountMemento.Mutations)
            {
                _ledgers.Add(InstantiateClassFromStringValue<Ledger>(mutation.Key, new Amount(mutation.Value)));
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
            RegisterEvent<AccountCreatedEvent>(onAccountCreated);
            RegisterEvent<AccountClosedEvent>(onAccountClosed);
            RegisterEvent<WithdrawlEvent>(onWithdrawl);
            RegisterEvent<DepositeEvent>(onDeposite);
        }

        private void onAccountCreated(AccountCreatedEvent accountCreatedEvent)
        {
            Id = accountCreatedEvent.AccountId;
        }

        private void onAccountClosed(AccountClosedEvent accountClosedEvent)
        {
            _closed = true;
        }

        private void onWithdrawl(WithdrawlEvent withdrawlEvent)
        {
            _ledgers.Add(new DebitMutation(withdrawlEvent.Amount));
            _balance = withdrawlEvent.Balance;
        }

        private void onDeposite(DepositeEvent depositeEvent)
        {
            _ledgers.Add(new CreditMutation(depositeEvent.Amount));
            _balance = depositeEvent.Balance;
        }
    }
}