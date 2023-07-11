using Fohjin.DDD.Domain.Mementos;
using Fohjin.DDD.Events.Account;
using Fohjin.DDD.EventStore;
using Fohjin.DDD.EventStore.Aggregate;
using Fohjin.DDD.EventStore.Storage.Memento;
using System.Diagnostics;

namespace Fohjin.DDD.Domain.Account;

public class ActiveAccount : BaseAggregateRoot<IDomainEvent>, IOriginator
{
    private readonly List<Ledger> _ledgers = new();

    private Guid _clientId;
    private AccountName _accountName;
    private AccountNumber _accountNumber;
    private Balance _balance;
    private bool _closed;

    public ActiveAccount()
    {
        Id = Guid.Empty;
        Version = 0;
        EventVersion = 0;
        _accountName = new AccountName(string.Empty);
        _accountNumber = new AccountNumber(string.Empty);
        _balance = new Balance();
        _closed = false;

        RegisterEvents();
    }

    private ActiveAccount(Guid clientId, string? accountName, string? accountNumber) : this()
    {
        Apply(new AccountOpenedEvent(Guid.NewGuid(), clientId, accountName, accountNumber));
    }

    public static ActiveAccount CreateNew(Guid clientId, string? accountName, string? accountNumber) =>
        new(clientId, accountName, accountNumber);

    public void ChangeAccountName(AccountName accountName)
    {
        Guard();

        Apply(new AccountNameChangedEvent(accountName.Name));
    }

    public ClosedAccount Close()
    {
        Guard();

        IsAccountBalanceZero();

        var closedAccount = ClosedAccount.CreateNew(Id, _clientId, _ledgers, _accountName, _accountNumber);
        Apply(new AccountClosedEvent());
        return closedAccount;
    }

    public void Withdrawal(Amount amount)
    {
        Guard();

        IsBalanceHighEnough(amount);

        var newBalance = _balance.Withdrawal(amount);

        Apply(new CashWithdrawnEvent(newBalance, amount));
    }

    public void Deposit(Amount amount)
    {
        Guard();

        var newBalance = _balance.Deposit(amount);

        Apply(new CashDepositedEvent(newBalance, amount));
    }

    public void ReceiveTransferFrom(AccountNumber sourceAccountNumber, Amount amount)
    {
        Guard();

        var newBalance = _balance.Deposit(amount);

        Apply(new MoneyTransferReceivedEvent(newBalance, amount, sourceAccountNumber.Number, _accountNumber.Number));
    }

    public void SendTransferTo(AccountNumber targetAccountNumber, Amount amount)
    {
        Guard();

        IsBalanceHighEnough(amount);

        var newBalance = _balance.Withdrawal(amount);

        Apply(new MoneyTransferSendEvent(newBalance, amount, _accountNumber.Number, targetAccountNumber.Number));
    }

    public void PreviousTransferFailed(AccountNumber accountNumber, Amount amount)
    {
        Guard();

        var newBalance = _balance.Deposit(amount);

        Apply(new MoneyTransferFailedEvent(newBalance, amount, accountNumber.Number));
    }

    private void Guard()
    {
        IsAccountNotCreated();
        IsAccountClosed();
    }

    private void IsAccountNotCreated()
    {
        if (Id == Guid.Empty)
            throw new NonExitsingAccountException("The ActiveAccount is not created and no operations can be executed on it");
    }

    private void IsAccountClosed()
    {
        if (_closed)
            throw new ClosedAccountException("The ActiveAccount is closed and no operations can be executed on it");
    }

    private void IsBalanceHighEnough(Amount amount)
    {
        if (_balance.WithdrawalWillResultInNegativeBalance(amount))
            throw new AccountBalanceToLowException(string.Format("The amount {0:C} is larger than your current balance {1:C}", (decimal)amount, (decimal)_balance));
    }

    private void IsAccountBalanceZero()
    {
        if (_balance != 0.0M)
            throw new AccountBalanceNotZeroException(string.Format("The current balance is {0:C} this must first be transferred to an other account", (decimal)_balance));
    }

    IMemento IOriginator.CreateMemento()
    {
        return new ActiveAccountMemento(Id, Version, _clientId, _accountName.Name, _accountNumber.Number, _balance, _ledgers, _closed);
    }

    void IOriginator.SetMemento(IMemento memento)
    {
        var activeAccountMemento = (ActiveAccountMemento)memento;
        Id = activeAccountMemento.Id;
        Version = activeAccountMemento.Version;
        _clientId = activeAccountMemento.ClientId;
        _accountName = new AccountName(activeAccountMemento.AccountName);
        _accountNumber = new AccountNumber(activeAccountMemento.AccountNumber);
        _balance = activeAccountMemento.Balance;
        _closed = activeAccountMemento.Closed;

        foreach (var ledger in activeAccountMemento.Ledgers)
        {
            var split = ledger.Value.Split(new[] { '|' });
            var amount = new Amount(Convert.ToDecimal(split[0]));
            var account = new AccountNumber(split[1]);
            var instance = InstantiateClassFromStringValue<Ledger>(ledger.Key, amount, account);
            if (instance != null)
                _ledgers.Add(instance);
        }
    }

    private TRequestedType? InstantiateClassFromStringValue<TRequestedType>(string className, params object[] constructorArguments)
    {
        var classType = GetType()
            .Assembly
            .GetExportedTypes()
            .Where(x => x.Name == className)
            .FirstOrDefault();

        if (classType == null)
            return default;

        return (TRequestedType?)Activator.CreateInstance(classType, constructorArguments);
    }

    private void RegisterEvents()
    {
        Debug.WriteLine($"{nameof(ActiveAccount)}::{nameof(RegisterEvents)}");
        RegisterEvent<AccountOpenedEvent>(OnAccountCreated);
        RegisterEvent<AccountClosedEvent>(OnAccountClosed);
        RegisterEvent<CashWithdrawnEvent>(OnWithdrawal);
        RegisterEvent<CashDepositedEvent>(OnDeposit);
        RegisterEvent<AccountNameChangedEvent>(OnAccountNameGotChanged);
        RegisterEvent<MoneyTransferReceivedEvent>(OnMoneyTransferredFromAnOtherAccount);
        RegisterEvent<MoneyTransferSendEvent>(OnMoneyTransferredToAnOtherAccount);
        RegisterEvent<MoneyTransferFailedEvent>(OnMoneyTransferFailed);
    }

    private void OnMoneyTransferFailed(MoneyTransferFailedEvent moneyTransferFailedEvent)
    {
        _ledgers.Add(new DebitTransferFailed(moneyTransferFailedEvent.Amount, new AccountNumber(string.Empty)));
        _balance = moneyTransferFailedEvent.Balance;
    }

    private void OnMoneyTransferredToAnOtherAccount(MoneyTransferSendEvent moneyTransferSendEvent)
    {
        _ledgers.Add(new CreditTransfer(moneyTransferSendEvent.Amount, new AccountNumber(moneyTransferSendEvent.TargetAccount)));
        _balance = moneyTransferSendEvent.Balance;
    }

    private void OnMoneyTransferredFromAnOtherAccount(MoneyTransferReceivedEvent moneyTransferReceivedEvent)
    {
        _ledgers.Add(new DebitTransfer(moneyTransferReceivedEvent.Amount, new AccountNumber(moneyTransferReceivedEvent.TargetAccount)));
        _balance = moneyTransferReceivedEvent.Balance;
    }

    private void OnAccountNameGotChanged(AccountNameChangedEvent accountNameChangedEvent)
    {
        _accountName = new AccountName(accountNameChangedEvent.AccountName);
    }

    private void OnAccountCreated(AccountOpenedEvent accountOpenedEvent)
    {
        Id = accountOpenedEvent.AccountId;
        _clientId = accountOpenedEvent.ClientId;
        _accountName = new AccountName(accountOpenedEvent.AccountName);
        _accountNumber = new AccountNumber(accountOpenedEvent.AccountNumber);
    }

    private void OnAccountClosed(AccountClosedEvent accountClosedEvent)
    {
        _closed = true;
    }

    private void OnWithdrawal(CashWithdrawnEvent cashWithdrawnEvent)
    {
        _ledgers.Add(new DebitMutation(cashWithdrawnEvent.Amount, new AccountNumber(string.Empty)));
        _balance = cashWithdrawnEvent.Balance;
    }

    private void OnDeposit(CashDepositedEvent cashDepositedEvent)
    {
        _ledgers.Add(new CreditMutation(cashDepositedEvent.Amount, new AccountNumber(string.Empty)));
        _balance = cashDepositedEvent.Balance;
    }
}