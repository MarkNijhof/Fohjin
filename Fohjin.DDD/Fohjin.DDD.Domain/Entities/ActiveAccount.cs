using System;
using System.Collections.Generic;
using System.Linq;
using Fohjin.DDD.Domain.Entities.Mementos;
using Fohjin.DDD.Domain.Exceptions;
using Fohjin.DDD.Domain.ValueObjects;
using Fohjin.DDD.Events.ActiveAccount;

namespace Fohjin.DDD.Domain.Entities
{
    public class ActiveAccount : BaseAggregateRoot, IOrginator
    {
        private AccountName _accountName;
        private AccountNumber _accountNumber;
        private Balance _balance;
        private readonly List<Ledger> _ledgers;
        private bool _closed;

        public ActiveAccount()
        {
            Id = new Guid();
            Version = 0;
            EventVersion = 0;
            _accountName = new AccountName(string.Empty);
            _accountNumber = new AccountNumber(string.Empty);
            _balance = new Balance();
            _ledgers = new List<Ledger>();
            _closed = false;

            registerEvents();
        }

        public static ActiveAccount CreateNew(Guid clientId, string accountName)
        {
            return new ActiveAccount(clientId, accountName);
        }

        public ActiveAccount(Guid clientId, string accountName) : this()
        {
            var accountNumber = SystemDateTime.Now().Ticks.ToString();
            Apply(new AccountCreatedEvent(Guid.NewGuid(), clientId, accountName, accountNumber));
        }

        public void ChangeAccountName(AccountName accountName)
        {
            Guard();

            Apply(new AccountNameGotChangedEvent(accountName.Name));
        }

        public ClosedAccount Close()
        {
            Guard();

            var closedAccount = new ClosedAccount(Id, _ledgers);
            Apply(new AccountClosedEvent());
            return closedAccount;
        }

        public void Withdrawl(Amount amount)
        {
            Guard();

            IsBalanceHighEnough(amount);

            var newBalance = _balance.Withdrawl(amount);

            Apply(new WithdrawlEvent(newBalance, amount));
        }

        public void Deposite(Amount amount)
        {
            Guard();

            var newBalance = _balance.Deposite(amount);

            Apply(new DepositeEvent(newBalance, amount));
        }

        public void ReceiveTransferFrom(AccountNumber accountNumber, Amount amount)
        {
            Guard();

            var newBalance = _balance.Deposite(amount);

            Apply(new MoneyTransferedFromAnOtherAccountEvent(newBalance, amount, accountNumber.Number));
        }

        public void SendTransferTo(AccountNumber accountNumber, Amount amount)
        {
            Guard();

            IsBalanceHighEnough(amount);

            var newBalance = _balance.Withdrawl(amount);

            Apply(new MoneyTransferedToAnOtherAccountEvent(newBalance, amount, accountNumber.Number));
        }

        private void Guard()
        {
            IsAccountCreated();
            IsAccountClosed();
        }

        private void IsAccountCreated()
        {
            if (Id == new Guid())
                throw new AccountWasNotCreatedException("The ActiveAcount is not created and no opperations can be executed on it");
        }

        private void IsAccountClosed()
        {
            if (_closed)
                throw new AccountWasClosedException("The ActiveAcount is closed and no opperations can be executed on it");
        }

        private void IsBalanceHighEnough(Amount amount)
        {
            if (_balance.WithdrawlWillResultInNegativeBalance(amount))
                throw new AccountBalanceIsToLowException(string.Format("The amount {0} is larger than your current balance {1}", (decimal)amount, (decimal)_balance));
        }

        IMemento IOrginator.CreateMemento()
        {
            return new ActiveAccountMemento(Id, Version, _accountName.Name, _accountNumber.Number, _balance, _ledgers, _closed);
        }

        void IOrginator.SetMemento(IMemento memento)
        {
            var accountMemento = (ActiveAccountMemento) memento;
            Id = accountMemento.Id;
            Version = accountMemento.Version;
            _accountName = new AccountName(accountMemento.AccountName);
            _accountNumber = new AccountNumber(accountMemento.AccountNumber);
            _balance = accountMemento.Balance;
            _closed = accountMemento.Closed;

            foreach (var ledger in accountMemento.Ledgers)
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

        private void registerEvents()
        {
            RegisterEvent<AccountCreatedEvent>(onAccountCreated);
            RegisterEvent<AccountClosedEvent>(onAccountClosed);
            RegisterEvent<WithdrawlEvent>(onWithdrawl);
            RegisterEvent<DepositeEvent>(onDeposite);
            RegisterEvent<AccountNameGotChangedEvent>(onAccountNameGotChanged);
            RegisterEvent<MoneyTransferedFromAnOtherAccountEvent>(onMoneyTransferedFromAnOtherAccount);
            RegisterEvent<MoneyTransferedToAnOtherAccountEvent>(onMoneyTransferedToAnOtherAccount);
        }

        private void onMoneyTransferedToAnOtherAccount(MoneyTransferedToAnOtherAccountEvent moneyTransferedToAnOtherAccountEvent)
        {
            _ledgers.Add(new CreditTransfer(moneyTransferedToAnOtherAccountEvent.Amount, moneyTransferedToAnOtherAccountEvent.OtherAccount));
            _balance = moneyTransferedToAnOtherAccountEvent.Balance;
        }

        private void onMoneyTransferedFromAnOtherAccount(MoneyTransferedFromAnOtherAccountEvent moneyTransferedFromAnOtherAccountEvent)
        {
            _ledgers.Add(new DebitTransfer(moneyTransferedFromAnOtherAccountEvent.Amount, moneyTransferedFromAnOtherAccountEvent.OtherAccount));
            _balance = moneyTransferedFromAnOtherAccountEvent.Balance;
        }

        private void onAccountNameGotChanged(AccountNameGotChangedEvent accountNameGotChangedEvent)
        {
            _accountName = new AccountName(accountNameGotChangedEvent.AccountName);
        }

        private void onAccountCreated(AccountCreatedEvent accountCreatedEvent)
        {
            Id = accountCreatedEvent.AccountId;
            _accountName = new AccountName(accountCreatedEvent.AccountName);
            _accountNumber = new AccountNumber(accountCreatedEvent.AccountNumber);
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