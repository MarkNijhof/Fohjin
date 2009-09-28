using System;
using System.Collections.Generic;
using System.Linq;
using Fohjin.DDD.Domain.Entities.ActiveAccountStates;
using Fohjin.DDD.Domain.Events;
using Fohjin.DDD.Domain.Memento;
using Fohjin.DDD.Domain.ValueObjects;

namespace Fohjin.DDD.Domain.Entities
{
    public class ActiveAccount : BaseAggregateRoot, IActiveAccount, IOrginator
    {
        private Balance _balance;
        private readonly List<Ledger> _ledgers;
        private IActiveAccountState _state;

        public ActiveAccount()
        {
            Id = new Guid();
            _balance = new Balance();
            _ledgers = new List<Ledger>();
            _state = new InValidState(Apply, () => Id, () => _balance, () => _ledgers);

            registerEvents();
        }

        void IActiveAccount.Create()
        {
            _state.Create();
        }

        ClosedAccount IActiveAccount.Close()
        {
            return _state.Close();
        }

        void IActiveAccount.Withdrawl(Amount amount)
        {
            _state.Withdrawl(amount);
        }

        void IActiveAccount.Deposite(Amount amount)
        {
            _state.Deposite(amount);
        }

        IMemento IOrginator.CreateMemento()
        {
            return new ActiveAccountMemento(Id, _balance, _ledgers, _state);
        }

        void IOrginator.SetMemento(IMemento memento)
        {
            var accountMemento = (ActiveAccountMemento) memento;
            Id = accountMemento.Id;
            _balance = accountMemento.Balance;

            foreach (var mutation in accountMemento.Mutations)
            {
                _ledgers.Add(InstantiateClassFromStringValue<Ledger>(mutation.Key, new Amount(mutation.Value)));
            }

            Func<Guid> idFunc = () => Id;
            Func<Balance> balanceFunc = () => _balance;
            Func<List<Ledger>> ledgersFunc = () => _ledgers;
            Action<IDomainEvent> applyAction = x => Apply(x);

            _state = InstantiateClassFromStringValue<IActiveAccountState>(accountMemento.State, applyAction, idFunc, balanceFunc, ledgersFunc);
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
            Id = accountCreatedEvent.Guid;
            _state = new CreatedState(Apply, () => Id, () => _balance, () => _ledgers);
        }

        private void onAccountClosed(AccountClosedEvent accountClosedEvent)
        {
            _state = new ClosedState(Apply, () => Id, () => _balance, () => _ledgers);
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