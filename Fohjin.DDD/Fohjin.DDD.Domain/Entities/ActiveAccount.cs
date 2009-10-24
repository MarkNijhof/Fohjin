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
        private Guid _clientId;
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

        private ActiveAccount(Guid clientId, string accountName) : this()
        {
            var accountNumber = SystemDateTime.Now().Ticks.ToString();
            Apply(new AccountCreatedEvent(Guid.NewGuid(), clientId, accountName, accountNumber));
        }

        public static ActiveAccount CreateNew(Guid clientId, string accountName)
        {
            return new ActiveAccount(clientId, accountName);
        }

        public void ChangeAccountName(AccountName accountName)
        {
            Guard();

            Apply(new AccountNameGotChangedEvent(accountName.Name));
        }

        public ClosedAccount Close()
        {
            Guard();

            IsAccountBalanceZero();

            var closedAccount = ClosedAccount.CreateNew(Id, _clientId, _ledgers, _accountName, _accountNumber);
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

        public void ReceiveTransferFrom(AccountNumber sourceAccountNumber, Amount amount)
        {
            Guard();

            var newBalance = _balance.Deposite(amount);

            Apply(new MoneyTransferReceivedFromAnOtherAccountEvent(newBalance, amount, sourceAccountNumber.Number, _accountNumber.Number));
        }

        public void SendTransferTo(AccountNumber targetAccountNumber, Amount amount)
        {
            Guard();

            IsBalanceHighEnough(amount);

            var newBalance = _balance.Withdrawl(amount);

            Apply(new MoneyTransferSendToAnOtherAccountEvent(newBalance, amount, _accountNumber.Number, targetAccountNumber.Number));
        }

        public void PreviousTransferFailed(AccountNumber accountNumber, Amount amount)
        {
            throw new NotImplementedException();
        }

        private void Guard()
        {
            IsAccountNotCreated();
            IsAccountClosed();
        }

        private void IsAccountNotCreated()
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

        private void IsAccountBalanceZero()
        {
            if (_balance != 0.0M)
                throw new AccountMustFirstBeEmptiedBeforeClosingException(string.Format("The current balance is {0} this must first be transfered to an other account", (decimal)_balance));
        }

        IMemento IOrginator.CreateMemento()
        {
            return new ActiveAccountMemento(Id, Version, _clientId, _accountName.Name, _accountNumber.Number, _balance, _ledgers, _closed);
        }

        void IOrginator.SetMemento(IMemento memento)
        {
            var accountMemento = (ActiveAccountMemento) memento;
            Id = accountMemento.Id;
            Version = accountMemento.Version;
            _clientId = accountMemento.ClientId;
            _accountName = new AccountName(accountMemento.AccountName);
            _accountNumber = new AccountNumber(accountMemento.AccountNumber);
            _balance = accountMemento.Balance;
            _closed = accountMemento.Closed;

            foreach (var ledger in accountMemento.Ledgers)
            {
                var split = ledger.Value.Split(new[] { '|' });
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
            RegisterEvent<AccountCreatedEvent>(onAccountCreated);
            RegisterEvent<AccountClosedEvent>(onAccountClosed);
            RegisterEvent<WithdrawlEvent>(onWithdrawl);
            RegisterEvent<DepositeEvent>(onDeposite);
            RegisterEvent<AccountNameGotChangedEvent>(onAccountNameGotChanged);
            RegisterEvent<MoneyTransferReceivedFromAnOtherAccountEvent>(onMoneyTransferedFromAnOtherAccount);
            RegisterEvent<MoneyTransferSendToAnOtherAccountEvent>(onMoneyTransferedToAnOtherAccount);
        }

        private void onMoneyTransferedToAnOtherAccount(MoneyTransferSendToAnOtherAccountEvent moneyTransferSendToAnOtherAccountEvent)
        {
            _ledgers.Add(new CreditTransfer(moneyTransferSendToAnOtherAccountEvent.Amount, new AccountNumber(moneyTransferSendToAnOtherAccountEvent.TargetAccount)));
            _balance = moneyTransferSendToAnOtherAccountEvent.Balance;
        }

        private void onMoneyTransferedFromAnOtherAccount(MoneyTransferReceivedFromAnOtherAccountEvent moneyTransferReceivedFromAnOtherAccountEvent)
        {
            _ledgers.Add(new DebitTransfer(moneyTransferReceivedFromAnOtherAccountEvent.Amount, new AccountNumber(moneyTransferReceivedFromAnOtherAccountEvent.TargetAccount)));
            _balance = moneyTransferReceivedFromAnOtherAccountEvent.Balance;
        }

        private void onAccountNameGotChanged(AccountNameGotChangedEvent accountNameGotChangedEvent)
        {
            _accountName = new AccountName(accountNameGotChangedEvent.AccountName);
        }

        private void onAccountCreated(AccountCreatedEvent accountCreatedEvent)
        {
            Id = accountCreatedEvent.AccountId;
            _clientId = accountCreatedEvent.ClientId;
            _accountName = new AccountName(accountCreatedEvent.AccountName);
            _accountNumber = new AccountNumber(accountCreatedEvent.AccountNumber);
        }

        private void onAccountClosed(AccountClosedEvent accountClosedEvent)
        {
            _closed = true;
        }

        private void onWithdrawl(WithdrawlEvent withdrawlEvent)
        {
            _ledgers.Add(new DebitMutation(withdrawlEvent.Amount, new AccountNumber(string.Empty)));
            _balance = withdrawlEvent.Balance;
        }

        private void onDeposite(DepositeEvent depositeEvent)
        {
            _ledgers.Add(new CreditMutation(depositeEvent.Amount, new AccountNumber(string.Empty)));
            _balance = depositeEvent.Balance;
        }
    }
}