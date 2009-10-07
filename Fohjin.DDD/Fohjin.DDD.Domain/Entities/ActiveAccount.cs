using System;
using System.Collections.Generic;
using System.Linq;
using Fohjin.DDD.Domain.Entities.ActiveAccountStates;
using Fohjin.DDD.Domain.Entities.Mementos;
using Fohjin.DDD.Domain.ValueObjects;
using Fohjin.DDD.Events;
using Fohjin.DDD.Events.ActiveAccount;

namespace Fohjin.DDD.Domain.Entities
{
    public class ActiveAccount : BaseAggregateRoot, IOrginator
    {
        private AccountName _accountName;
        private Balance _balance;
        private readonly List<Ledger> _ledgers;
        private IActiveAccountState _state;

        public ActiveAccount()
        {
            Id = new Guid();
            Version = 0;
            EventVersion = 0;
            _accountName = new AccountName(string.Empty);
            _balance = new Balance();
            _ledgers = new List<Ledger>();
            _state = new InValidState(Apply, () => Id, () => _accountName, () => _balance, () => _ledgers);

            registerEvents();
        }

        public void Create(Guid id, AccountName accountName)
        {
            _state.Create(id, accountName);
        }

        public ClosedAccount Close()
        {
            return _state.Close();
        }

        public void Withdrawl(Amount amount)
        {
            _state.Withdrawl(amount);
        }

        public void Deposite(Amount amount)
        {
            _state.Deposite(amount);
        }

        IMemento IOrginator.CreateMemento()
        {
            return new ActiveAccountMemento(Id, Version, _accountName.Name, _balance, _ledgers, _state);
        }

        void IOrginator.SetMemento(IMemento memento)
        {
            var accountMemento = (ActiveAccountMemento) memento;
            Id = accountMemento.Id;
            Version = accountMemento.Version;
            _balance = accountMemento.Balance;
            _accountName = new AccountName(accountMemento.AccountName);

            foreach (var mutation in accountMemento.Mutations)
            {
                _ledgers.Add(InstantiateClassFromStringValue<Ledger>(mutation.Key, new Amount(mutation.Value)));
            }

            Func<Guid> idFunc = () => Id;
            Func<AccountName> accountNameFunc = () => _accountName;
            Func<Balance> balanceFunc = () => _balance;
            Func<List<Ledger>> ledgersFunc = () => _ledgers;
            Action<IDomainEvent> applyAction = x => Apply(x);

            _state = InstantiateClassFromStringValue<IActiveAccountState>(accountMemento.State, applyAction, idFunc, accountNameFunc, balanceFunc, ledgersFunc);
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
            _state = new CreatedState(Apply, () => Id, () => _accountName, () => _balance, () => _ledgers);
        }

        private void onAccountClosed(AccountClosedEvent accountClosedEvent)
        {
            _state = new ClosedState(Apply, () => Id, () => _accountName, () => _balance, () => _ledgers);
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